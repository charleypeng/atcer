// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System.Web;
using ATCer.Authorization.Dtos;
using ATCer.Swagger.Dtos;
using ATCer.Swagger.Services;

namespace ATCer.Swagger.Client.Services
{
    [ScopedService]
    public class SwaggerService : ISwaggerService
    {
        private readonly static string controller = "swagger";
        private readonly IApiCaller apiCaller;

        public SwaggerService(IApiCaller apiCaller)
        {
            this.apiCaller = apiCaller;
        }

        public async Task<SwaggerModel> Analysis(string url)
        {
            url = HttpUtility.UrlEncode(url);
            return await apiCaller.GetAsync<SwaggerModel>($"{controller}/analysis/{url}");
        }

        public async Task<List<SwaggerSpecificationOpenApiInfoDto>> GetApiGroup()
        {
            return await apiCaller.GetAsync<List<SwaggerSpecificationOpenApiInfoDto>>($"{controller}/api-group");
        }

        public async Task<List<ApiEndpoint>> GetFunctionsFromJson(string url)
        {
            url = HttpUtility.UrlEncode(url);
            return await apiCaller.GetAsync<List<ApiEndpoint>>($"{controller}/functions-from-json/{url}");
        }
    }
}
