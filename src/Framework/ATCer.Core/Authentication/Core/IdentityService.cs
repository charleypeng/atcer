// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion.FriendlyException;
using ATCer.Authentication.Dtos;
using ATCer.Authentication.Enums;
using ATCer.Enums;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

#nullable disable
namespace ATCer.Authentication.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class IdentityService : IIdentityService
    {
        /// <summary>
        /// 请求上下文访问器
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        private Identity _identity;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public IdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// 获取身份
        /// </summary>
        /// <returns></returns>
        public Identity GetIdentity()
        {
            if (_identity != null)
            {
                return _identity;
            }
            _identity= GetIdentityFromContext();
            return _identity;
        }


        /// <summary>
        /// 获取身份
        /// </summary>
        /// <returns></returns>
        private Identity GetIdentityFromContext()
        {
            if (_httpContextAccessor.HttpContext == null)
            {
                //非http请求
                return null;
            }
            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                return null;
            }
            string tokenTypeKey = _httpContextAccessor.HttpContext.User.FindFirstValue(AuthKeyConstants.TokenTypeKey);
            if (JwtTokenType.RefreshToken.ToString().Equals(tokenTypeKey))
            {
                throw Oops.Oh(ExceptionCode.REFRESHTOKEN_CANNOT_USED_IN_AUTHENTICATION);
            }
            Identity identity = new Identity();
            identity.Id = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            identity.Name = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            identity.GivenName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.GivenName);
            string identityType = _httpContextAccessor.HttpContext.User.FindFirstValue(AuthKeyConstants.IdentityType);
            identity.IdentityType = Enum.Parse<IdentityType>(identityType, true);
            identity.LoginId = _httpContextAccessor.HttpContext.User.FindFirstValue(AuthKeyConstants.ClientIdKeyName);
            string loginClientType = _httpContextAccessor.HttpContext.User.FindFirstValue(AuthKeyConstants.ClientTypeKeyName);
            identity.LoginClientType = Enum.Parse<LoginClientType>(loginClientType, true);
            return identity;
        }
    }
}
