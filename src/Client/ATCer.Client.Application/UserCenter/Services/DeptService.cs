// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using ATCer.Client.Base;
using ATCer.UserCenter.Dtos;
using ATCer.UserCenter.Services;

namespace ATCer.UserCenter.Client.Services
{
    /// <summary>
    /// 部门服务
    /// </summary>
    [ScopedService]
    public class DeptService : ClientServiceBase<DeptDto>, IDeptService
    {
        public DeptService(IApiCaller apiCaller) : base(apiCaller, "dept")
        {
        }

        public async Task<string> GetSeedData()
        {
            return await apiCaller.GetAsync<string>($"{controller}/seed-data");
        }

        public async Task<List<DeptDto>> GetTree(bool includeLocked = false)
        {
            return await apiCaller.GetAsync<List<DeptDto>>($"{controller}/tree/{includeLocked}");
        }
    }
}
