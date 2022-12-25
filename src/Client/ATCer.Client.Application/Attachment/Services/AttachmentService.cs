// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Attachment.Dtos;
using ATCer.Attachment.Services;
using ATCer.Base;
using ATCer.Client.Base;
using Microsoft.AspNetCore.Http;

namespace ATCer.Attachment.Client.Services
{
    [ScopedService]
    public class AttachmentService : ClientServiceBase<AttachmentDto, Guid>, IAttachmentService
    {

        public AttachmentService(IApiCaller apiCaller) : base(apiCaller, "attachment")
        {
        }

        public async Task<Base.MyPagedList<AttachmentDto>> Search(int? businessType, int? fileType, string businessId, string order = "desc", int pageIndex = 1, int pageSize = 10)
        {
            IDictionary<string, object> pramas = new Dictionary<string, object>()
            {
                {"businessType",businessType },
                {"fileType",fileType },
                {"order",order }
            };
            return await apiCaller.GetAsync<MyPagedList<AttachmentDto>>($"{controller}/search/{pageIndex}/{pageSize}", pramas);
        }

        public Task<UploadAttachmentOutput> Upload(UploadAttachmentInput input, IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}
