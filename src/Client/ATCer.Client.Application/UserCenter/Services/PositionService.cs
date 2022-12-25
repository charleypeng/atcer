// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Base;
using ATCer.Client.Base;
using ATCer.UserCenter.Dtos;
using ATCer.UserCenter.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATCer.UserCenter.Client.Services
{
    [ScopedService]
    public class PositionService : ClientServiceBase<PositionDto, int>, IPositionService
    {
        public PositionService(IApiCaller apiCaller) : base(apiCaller, "position")
        {
        }
        
        public async Task<MyPagedList<PositionDto>> Search(string name, int pageIndex = 1, int pageSize = 10)
        {
            IDictionary<string, object> pramas = new Dictionary<string, object>()
            {
                {"name",name }
            };
            return await apiCaller.GetAsync<MyPagedList<PositionDto>>($"{controller}/search/{pageIndex}/{pageSize}", pramas);
        }
    }
}
