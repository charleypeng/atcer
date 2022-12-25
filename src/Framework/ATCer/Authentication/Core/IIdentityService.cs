// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Authentication.Dtos;

namespace ATCer.Authentication.Core
{
    /// <summary>
    /// 身份认证
    /// </summary>
    public interface IIdentityService
    {
        /// <summary>
        /// 获取身份
        /// </summary>
        /// <returns></returns>
        Identity GetIdentity();
    }
}
