// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System.ComponentModel;

namespace ATCer.Authentication.Enums
{
    /// <summary>
    /// 请求登录的客户端类型
    /// </summary>
    public enum LoginClientType
    {
        /// <summary>
        /// 浏览器类型
        /// </summary>
        [Description("浏览器")]
        Browser,

        /// <summary>
        /// 桌面客户端
        /// </summary>
        [Description("桌面")]
        Desktop,

        /// <summary>
        /// 手机客户端
        /// </summary>
        [Description("手机")]
        Mobile,

        /// <summary>
        /// 服务端
        /// </summary>
        [Description("服务端")]
        Server,
        /// <summary>
        /// APP
        /// </summary>
        [Description("程序端")]
        App
    }
}
