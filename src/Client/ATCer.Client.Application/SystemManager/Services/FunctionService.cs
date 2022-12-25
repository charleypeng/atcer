// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System.Web;
using ATCer.Client.Base;
using ATCer.SystemManager.Dtos;
using ATCer.SystemManager.Services;
using MyHttpMethod = ATCer.Enums.MyHttpMethod;

namespace ATCer.SystemManager.Client.Services
{
    /// <summary>
    /// 
    /// </summary>
    [ScopedService]
    public class FunctionService : ClientServiceBase<FunctionDto, Guid>, IFunctionService
    {
        public FunctionService(IApiCaller apiCaller) : base(apiCaller, "function")
        {
        }

        public async Task<bool> EnableAudit(Guid id, bool enableAudit=true)
        {
            return await apiCaller.PutAsync<bool, bool>($"{controller}/{id}/enable-audit/{enableAudit}");
        }

        public async Task<bool> Exists(MyHttpMethod method, string path)
        {
            path=HttpUtility.UrlEncode(path);
            return await apiCaller.GetAsync<bool>($"{controller}/exists/{method}/{path}");
        }

        public async Task<FunctionDto> GetByKey(string key)
        {
            return await apiCaller.GetAsync<FunctionDto>($"{controller}/by-key/{key}");
        }
    }
}
