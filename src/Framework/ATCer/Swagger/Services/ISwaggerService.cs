// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Authorization.Dtos;
using ATCer.Swagger.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATCer.Swagger.Services
{
    /// <summary>
    /// swagger 服务
    /// </summary>
    public interface ISwaggerService
    {
        /// <summary>
        /// 解析open api json
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<SwaggerModel> Analysis(string url);

        /// <summary>
        /// 获取 swagger 配置
        /// </summary>
        /// <returns></returns>
        Task<List<SwaggerSpecificationOpenApiInfoDto>> GetApiGroup();

        /// <summary>
        /// 从json中获取function
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<List<ApiEndpoint>> GetFunctionsFromJson(string url);
    }
}
