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
    [ScopedService]
    public class ResourceService : ClientServiceBase<ResourceDto,Guid>,IResourceService
    {
        public ResourceService(IApiCaller apiCaller):base(apiCaller, "resource")
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<ResourceDto>> GetChildren(Guid id)
        {
            return await apiCaller.GetAsync<List<ResourceDto>>($"{controller}/{id}/children");
        }

        public async Task<List<FunctionDto>> GetFunctions(Guid id)
        {
            return await apiCaller.GetAsync<List<FunctionDto>>($"{controller}/{id}/functions");
        }

        public async Task<List<ResourceDto>> GetRoot()
        {
            return await apiCaller.GetAsync<List<ResourceDto>>($"{controller}/root");
        }

        public async Task<List<ResourceDto>> GetTree(bool includLocked = true, string rootKey = null)
        {
            IDictionary<string, object> queryString = new Dictionary<string, object>();
            queryString.Add(nameof(rootKey), rootKey);
            queryString.Add(nameof(includLocked), includLocked);
            return await apiCaller.GetAsync<List<ResourceDto>>($"{controller}/tree", queryString);
        }
    }
}
