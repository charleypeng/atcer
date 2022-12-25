// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Attachment.Dtos;
using ATCer.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace ATCer.Attachment.Services
{
    /// <summary>
    /// 附件服务接口
    /// </summary>
    public interface IAttachmentService : IServiceBase<AttachmentDto, Guid>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        Task<UploadAttachmentOutput> Upload(UploadAttachmentInput input, IFormFile file);
    }
}