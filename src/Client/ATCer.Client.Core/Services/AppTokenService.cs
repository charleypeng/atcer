// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Authentication.Dtos;
using ATCer.Client.Base;
using ATCer.SystemManager.Dtos;
using ATCer.UserCenter.Dtos;
using ATCer.UserCenter.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATCer.Client.Services
{
    public class AppTokenService : ClientServiceBase<AppTokenDto, string>, IAppTokenServce
    {
        public AppTokenService(IApiCaller apiCaller):base(apiCaller, "app-token")
        { }
        
        public Task<string> GenerateToken()
        {
            throw new NotImplementedException();
        }

        public Task<List<FunctionDto>> GetFunctions(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Identity> GetIdentity(string appToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetJwtToken(string appToken)
        {
            throw new NotImplementedException();
        }

        public Task ImportToCache()
        {
            throw new NotImplementedException();
        }
    }
}
