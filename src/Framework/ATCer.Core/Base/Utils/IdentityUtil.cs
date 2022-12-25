// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion;
using ATCer.Authentication.Core;
using ATCer.Authentication.Dtos;
using ATCer.Authentication.Enums;

namespace ATCer.Base
{
    /// <summary>
    /// 身份快捷操作
    /// </summary>
    public class IdentityUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Identity GetIdentity()
        {
            IIdentityService identityService = App.GetService<IIdentityService>();

            return identityService.GetIdentity();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetIdentityId()
        {
            return GetIdentity()?.Id;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IdentityType GetIdentityType()
        {
            Identity identity = GetIdentity();
            return identity==null? IdentityType.Unknown: identity.IdentityType;
        }
    }
}
