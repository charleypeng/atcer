// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Furion.DynamicApiController;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ATCer.SystemManager.Dtos;
using ATCer.Base.Domains;
using ATCer.Common;

namespace ATCer.SystemManager.Services
{

    /// <summary>
    /// 资源与接口关系服务
    /// </summary>
    [ApiDescriptionSettings("SystemBaseServices")]
    public class ResourceFunctionService :  IResourceFunctionService, IDynamicApiController
    {
        private readonly IRepository<ResourceFunction> _resourceFunctionRespository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceFunctionRespository"></param>
        public ResourceFunctionService(IRepository<ResourceFunction> resourceFunctionRespository)
        {
            _resourceFunctionRespository = resourceFunctionRespository;
        }

        /// <summary>
        /// 添加资源与接口关系
        /// </summary>
        /// <param name="resourceFunctionDtos"></param>
        /// <returns></returns>
        public async Task<bool> Add(List<ResourceFunctionDto> resourceFunctionDtos)
        {
            await _resourceFunctionRespository.InsertAsync(resourceFunctionDtos.Select(x => x.Adapt<ResourceFunction>()));
            return true;
        }

        /// <summary>
        /// 删除资源与接口关系
        /// </summary>
        /// <param name="resourceId"></param>
        /// <param name="functionId"></param>
        /// <returns></returns>
        public async Task<bool> Delete([FromRoute] Guid resourceId, [FromRoute] Guid functionId)
        {
            List<ResourceFunction> entitys = await _resourceFunctionRespository.Where(x => x.ResourceId.Equals(resourceId) && x.FunctionId.Equals(functionId)).ToListAsync();

            if (entitys == null || entitys.Count == 0)
            {
                return false;
            }

            await _resourceFunctionRespository.DeleteAsync(entitys);

            return true;
        }

        /// <summary>
        /// 获取种子数据
        /// </summary>
        /// <param name="resourceIds"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> GetSeedData([FromBody] List<Guid> resourceIds)
        {
            if (resourceIds == null) { 
                resourceIds=new List<Guid>(0);
            }
            List<ResourceFunction> list = await _resourceFunctionRespository.Where(x=> resourceIds.Any(r=>r.Equals(x.ResourceId))).OrderBy(x=>x.ResourceId).ToListAsync();
            return SeedDataGenerateTool.Generate(list,typeof(ResourceFunction).Name);
        }
    }
}
