// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Client.Base;
using ATCer.SystemManager.Dtos;
using ATCer.SystemManager.Services;

namespace ATCer.SystemManager.Client.Services
{
    /// <summary>
    /// 
    /// </summary>
    [ScopedService]
    public class ResourceFunctionService : IResourceFunctionService
    {
        private readonly static string controller = "resource-function";
        private readonly IApiCaller apiCaller;
        public ResourceFunctionService(IApiCaller apiCaller)
        {
            this.apiCaller = apiCaller;
        }

        public async Task<bool> Add(List<ResourceFunctionDto> resourceFunctionDtos)
        {
            return await apiCaller.PostAsync<List<ResourceFunctionDto>, bool>($"{controller}", resourceFunctionDtos);
        }

        public async Task<bool> Delete(Guid resourceId, Guid functionId)
        {
            return await apiCaller.DeleteAsync<bool>($"{controller}/{resourceId}/{functionId}");
        }

        public async Task<string> GetSeedData(List<Guid> resourceIds)
        {
            return await apiCaller.PostAsync<List<Guid>, string>($"{controller}/seed-data", resourceIds);
        }
    }
}
