﻿// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Client.Base;
using ATCer.Client.Base.Authorization;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace ATCer.Client.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class ClientUIResourceAuthorizationHandler : AuthorizationHandler<ClientUIAuthorizationRequirement>
    {

        private readonly IAuthenticationStateManager authenticationStateManager;

        public ClientUIResourceAuthorizationHandler(IAuthenticationStateManager authenticationStateManager)
        {
            this.authenticationStateManager = authenticationStateManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ClientUIAuthorizationRequirement requirement)
        {
            if (context.Resource is ClientResource resource)
            {
                bool state = false;

                state = resource.AndCondition ? true : false;

                foreach (string key in resource.Keys)
                {
                    var isAuth = await authenticationStateManager.CheckCurrentUserHaveBtnResourceKey(key);
                    if (resource.AndCondition)
                    {
                        if (!isAuth)
                        {
                            state = false;
                        }
                    }
                    else
                    {
                        if (isAuth)
                        {
                            state = true;
                        }
                    }
                }

                if (state)
                {
                    //如果当前用户有资源访问权限，则返回成功
                    context.Succeed(requirement);
                }
            }
        }
    }
}
