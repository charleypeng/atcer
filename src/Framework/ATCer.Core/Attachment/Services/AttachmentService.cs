﻿// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Mapster;
using System.IO;
using System;
using Furion.FriendlyException;
using ATCer.Enums;
using ATCer.Attachment.Dtos;
using ATCer.FileStore;
using ATCer.Attachment.Core;
using Furion.DependencyInjection;

namespace ATCer.Attachment.Services
{
    /// <summary>
    /// 附件服务
    /// </summary>
    [ApiDescriptionSettings("SystemBaseServices")]
    public class AttachmentService : ServiceBase<Domains.Attachment, AttachmentDto, Guid>, IAttachmentService, IScoped
    {
        private readonly IFileStoreService fileStoreService;
        private readonly IRepository<Domains.Attachment> repository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="fileStoreService"></param>
        public AttachmentService(IRepository<Domains.Attachment> repository, IFileStoreService fileStoreService) : base(repository)
        {
            this.fileStoreService = fileStoreService;
            this.repository = repository;
        }
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <remarks>
        /// 上传单个附件
        /// </remarks>
        /// <param name="input"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<UploadAttachmentOutput> Upload([FromForm] UploadAttachmentInput input, IFormFile file)
        {
            if (file == null) throw Oops.Oh(ExceptionCode.NO_INCLUD_FILE);

            UploadAttachmentOutput uploadOutput = new UploadAttachmentOutput();
            AttachmentDto attachment = new AttachmentDto();
            input.Adapt(attachment);
            attachment.ContentType = file.ContentType;
            attachment.FileType = FileTypeDistinguishHelper.GetAttachmentFileType(file.ContentType);
            attachment.OriginalName = file.FileName;
            attachment.Size = file.Length;
            attachment.Suffix = Path.GetExtension(file.FileName).ToLower();
            string fileName = (Guid.NewGuid().ToString() + Path.GetExtension(file.FileName)).ToLower();
            attachment.Name = fileName;
            string path = $"{input.BusinessType}/{DateTime.Now.ToString("yyyMMdd")}/".ToLower();
            attachment.Path = path;
            string url = await fileStoreService.Save(file.OpenReadStream(), path + fileName);
            attachment.Url = url;
            attachment.CreatedTime = DateTime.Now;
            var entity = await base.Insert(attachment);
            uploadOutput.Url = url;
            uploadOutput.Id = entity.Id;
            return uploadOutput;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [NonAction]
        public override Task<AttachmentDto> Insert(AttachmentDto input)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [NonAction]
        public override Task<bool> Update(AttachmentDto input)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <remarks>
        /// 根据主键删除
        /// </remarks>
        /// <param name="id"></param>
        public override async Task<bool> Delete(Guid id)
        {
            Domains.Attachment attachment = await repository.FindAsync(id);
            if (attachment == null) return false;
            await repository.DeleteAsync(attachment);
            fileStoreService.Delete(Path.Combine(attachment.Path, attachment.Name));
            return true;
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <remarks>
        /// 根据主键批量删除
        /// </remarks>
        /// <param name="ids"></param>
        [HttpPost]
        public override async Task<bool> Deletes(Guid[] ids)
        {
            foreach (Guid id in ids)
            {
                if (!await Delete(id)) { return false; }
            }
            return true;
        }
    }
}
