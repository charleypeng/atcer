// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Client.Base;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ATCer.Client.Core
{
    [ScopedService]
    public  class HttpClientManager
    {
        private readonly HttpClient httpClient;

        public HttpClientManager(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public void SetClientAuthorization(string token)
        {
            //httpClient.Authenticator = new JwtAuthenticator(token);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        }
    }
}
