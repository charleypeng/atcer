// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System.ComponentModel;

namespace ATCer.Authentication.Enums
{
    /// <summary>
    /// 身份类型
    /// </summary>
    public enum IdentityType
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        Unknown,
        /// <summary>
        /// 用户
        /// </summary>
        [Description("用户")]
        User,
        /// <summary>
        /// 客户端
        /// </summary>
        [Description("客户端")]
        Client,
        /// <summary>
        /// AppToken
        /// </summary>
        [Description("App密钥")]
        AppToken
    }
}
