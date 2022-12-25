// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.HRCenter.Dtos;
using ATCer.HRCenter.Services;
using ATCer.Attachment.Dtos;
using ATCer.Base;
using ATCer.Client.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.HRCenter.Client.Services
{
    [ScopedService]
    public class TimeItemService : ClientServiceBase<TimeItemDto, long>, ITimeItemService
    {
        public TimeItemService(IApiCaller apiCaller) : base(apiCaller, "time-item")
        {

        }

        public async Task<bool> BulkInsert(IEnumerable<TimeItemDto> input)
        {
            return await apiCaller.PostAsync<IEnumerable<TimeItemDto>, bool>($"{controller}/bulk-insert", input );
        }

        public async Task<bool> DeleteRecentImported()
        {
            return await apiCaller.DeleteAsync<bool>($"{controller}/recent-imported");
        }

        public async Task<IEnumerable<TimeItemDto>> GetImported()
        {
            return await apiCaller.GetAsync<IEnumerable<TimeItemDto>>($"{controller}/imported");
        }

        public async Task<MyPagedList<TimeItemDto>> GetImported(int pageIndex = 1, int pageSize = 10)
        {
            return await apiCaller.GetAsync<MyPagedList<TimeItemDto>>($"{controller}/imported/{pageIndex}/{pageSize}");
        }

        public async Task<bool> ImportNow()
        {
            var result = await apiCaller.GetAsync<bool>($"{controller}/import-now");
            return result;
        }

        public async Task<ImportFromExcelOutput> ImportViaFile(UploadAttachmentInput input, IFormFile file)
        {
            throw new NotImplementedException();
        }

        public async Task<ImportFromExcelOutput> ImportViaId(Guid id)
        {
            return await apiCaller.PostAsync<Guid, ImportFromExcelOutput>($"{controller}/import-via-id", id);
        }
    }
}
