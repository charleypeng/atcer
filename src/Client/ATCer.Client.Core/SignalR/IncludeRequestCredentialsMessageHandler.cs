// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Microsoft.AspNetCore.Components.WebAssembly.Http;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ATCer.Client.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class IncludeRequestCredentialsMessageHandler : DelegatingHandler
    {
        private readonly Action<HttpRequestMessage> _httpRequestConfigure;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="innerHandler"></param>
        public IncludeRequestCredentialsMessageHandler(HttpMessageHandler innerHandler) : base(innerHandler)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="innerHandler"></param>
        /// <param name="httpRequestConfigure"></param>
        public IncludeRequestCredentialsMessageHandler(HttpMessageHandler innerHandler,Action<HttpRequestMessage> httpRequestConfigure) : base(innerHandler)
        {
            _httpRequestConfigure  = httpRequestConfigure;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (_httpRequestConfigure != null) 
            {
                _httpRequestConfigure.Invoke(request);
            }

            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            request.SetBrowserRequestMode(BrowserRequestMode.Cors);
            return base.SendAsync(request, cancellationToken);
        }
    }
}
