// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion.DynamicApiController;
using ATCer.UserCenter.Services;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.Core.UserCenter.Services
{
    [AllowAnonymous]
    public class Test2Service:IDynamicApiController
    {
        private readonly IAppTokenServce appTokenService;
        public Test2Service(IAppTokenServce appTokenService)
        {
            this.appTokenService = appTokenService; 
        }

        public async Task<object> SaveMe()
        {
            var t = await appTokenService.GetAll();
            return t;
        }
    }
}
