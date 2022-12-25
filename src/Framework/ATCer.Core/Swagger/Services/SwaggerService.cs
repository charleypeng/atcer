// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion;
using Furion.DynamicApiController;
using Furion.FriendlyException;
using Furion.RemoteRequest.Extensions;
using Furion.SpecificationDocument;
using ATCer.Authorization.Dtos;
using ATCer.Common;
using ATCer.Enums;
using ATCer.Swagger.Dtos;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace ATCer.Swagger.Services
{
    /// <summary>
    /// Swagger服务
    /// </summary>
    [ApiDescriptionSettings("SystemBaseServices")]
    public class SwaggerService : ISwaggerService, IDynamicApiController
    {


        /// <summary>
        /// 解析api json
        /// </summary>
        /// <remarks> swagger json 文件解析功能</remarks>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<SwaggerModel> Analysis(string url)
        {
            url = HttpUtility.UrlDecode(url);

            var swaggerInfo = await url.OnException((httpClient, response,msg) =>
            {

                if (!response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    throw Oops.Bah(ExceptionCode.REQUEST_URL_IS_INVALID);
                }

            }).GetAsAsync<SwaggerModel>();

            return swaggerInfo;
        }
        /// <summary>
        /// 获取 swagger 配置
        /// </summary>
        /// <remarks>
        /// 获取api分组设置
        /// </remarks>
        /// <returns></returns>
        public Task<List<SwaggerSpecificationOpenApiInfoDto>> GetApiGroup()
        {
            // 载入配置
            SpecificationDocumentSettingsOptions options = App.GetOptions<SpecificationDocumentSettingsOptions>();
            if (options == null) return null;
            return Task.FromResult(options.GroupOpenApiInfos.Select(x => x.Adapt<SwaggerSpecificationOpenApiInfoDto>()).ToList());
        }
        /// <summary>
        /// 从json中获取function
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<List<ApiEndpoint>> GetFunctionsFromJson(string url)
        {
            List<ApiEndpoint> _functionDtos = new List<ApiEndpoint>();
            SwaggerModel swaggerModel = await Analysis(url);
            if (swaggerModel != null && swaggerModel.paths != null)
            {
                Dictionary<string, SwaggerTagInfo> tagMap = swaggerModel.tags.ToDictionary<SwaggerTagInfo, string>(x => x.name);
                foreach (var item in swaggerModel.paths)
                {
                    foreach (var m in item.Value)
                    {
                        string tags = m.Value.tags == null ? null : string.Join("_", m.Value.tags.Select(x => tagMap.ContainsKey(x) ? tagMap[x].description : x));
                        ApiEndpoint function = new ApiEndpoint()
                        {
                            Path = item.Key,
                            Method = (MyHttpMethod)Enum.Parse(typeof(MyHttpMethod), m.Key.ToUpper()),
                            Key = MD5Encryption.Encrypt(item.Key + m.Key.ToUpper()),
                            Summary = m.Value.summary,
                            Description = m.Value.description,
                            //Group = _selectedGroup.Title,
                            Service = tags,
                            EnableAudit = true
                        };
                        if (MyHttpMethod.GET.Equals(function.Method))
                        {
                            function.EnableAudit = false;
                        }
                        _functionDtos.Add(function);
                    }
                }
            }
            return _functionDtos;
        }
    }
}
