// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Authorization.Dtos;
using ATCer.Client.Base;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATCer.Client.Core
{
    public class SignalRClientBuilder : ISignalRClientBuilder
    {
        private string _clientName = string.Empty;
        private string _url = string.Empty;
        private Func<Task<string?>>? _accessTokenProvider;
        private bool _enableAutomaticReconnect = true;
        private IClientLogger _clientLogger;
        private Dictionary<string, string> _headers = new Dictionary<string,string>();
        private readonly IAuthenticationStateManager _authenticationStateManager;
        private readonly IOptions<ApiSettings> _options;

        public SignalRClientBuilder(IClientLogger clientLogger, IAuthenticationStateManager authenticationStateManager, IOptions<ApiSettings> options)
        {
            _clientLogger = clientLogger;
            _authenticationStateManager = authenticationStateManager;
            _options = options;
        }
        public ISignalRClientBuilder GetInstance()
        {
            return new SignalRClientBuilder(_clientLogger, _authenticationStateManager, _options);
        }
        public ISignalRClient Build()
        {
            if (string.IsNullOrWhiteSpace(_clientName))
            {
                throw new ArgumentNullException("clientName");
            }
            if (string.IsNullOrWhiteSpace(_url))
            {
                throw new ArgumentNullException("url");
            }
            if (_accessTokenProvider == null)
            {
                _accessTokenProvider = async () =>
                 {
                     TokenOutput token = await _authenticationStateManager.GetCurrentToken();
                     return token.AccessToken;
                 };
            }
            if (_url.IndexOf(":") == -1)
            {
                _url = $"{_options.Value.BaseAddres}{_url}";
            }
            SignalRClient signalRClient = new SignalRClient(_clientName,_url, _clientLogger, _accessTokenProvider, _headers);
            signalRClient.AutomaticReconnect(_enableAutomaticReconnect);
            return signalRClient;
        }

        public ISignalRClientBuilder SetHeader(Dictionary<string,string> headers)
        {
            this._headers = headers;
            return this;
        }

        public ISignalRClientBuilder SetAccessTokenProvider(Func<Task<string?>>? accessTokenProvider)
        {
            this._accessTokenProvider = accessTokenProvider;
            return this;
        }

        public ISignalRClientBuilder SetClientName(string clientName)
        {
            this._clientName = clientName;
            return this;
        }

        public ISignalRClientBuilder SetEnableAutomaticReconnect(bool enableAutomaticReconnect = true)
        {
            this._enableAutomaticReconnect = enableAutomaticReconnect;
            return this;
        }

        public ISignalRClientBuilder SetLogger(IClientLogger clientLogger)
        {
            this._clientLogger = clientLogger;
            return this;
        }

        public ISignalRClientBuilder SetUrl(string url)
        {
            this._url = url;
            return this;
        }
    }
}
