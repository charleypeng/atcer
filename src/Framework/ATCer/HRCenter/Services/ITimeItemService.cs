// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.HRCenter.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATCer.Attachment.Dtos;

namespace ATCer.HRCenter.Services
{
    /// <summary>
    /// 执勤小时服务接口
    /// </summary>
    public interface ITimeItemService: ATCer.Base.IServiceBase<TimeItemDto, long>
    {
        /// <summary>
        /// 从Excel导入
        /// </summary>
        /// <param name="input"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        Task<ImportFromExcelOutput> ImportViaFile(UploadAttachmentInput input, IFormFile file);
        /// <summary>
        /// 批量导入
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> BulkInsert(IEnumerable<TimeItemDto> input);
        /// <summary>
        /// 从id导入
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ImportFromExcelOutput> ImportViaId(Guid id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<ATCer.Base.MyPagedList<TimeItemDto>> GetImported(int pageIndex = 1, int pageSize = 10);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<bool> ImportNow();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<bool> DeleteRecentImported();
    }
}
