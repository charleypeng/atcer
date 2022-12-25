// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Authentication.Enums;
using System.ComponentModel;

namespace ATCer.Authentication.Dtos
{
    /// <summary>
    /// 身份信息
    /// </summary>
    [Description("身份信息")]
    public class Identity
    {
        /// <summary>
        /// 身份唯一编号
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 身份唯一名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 身份昵称
        /// </summary>
        public string GivenName { get; set; }
        /// <summary>
        /// 身份类型
        /// </summary>
        public IdentityType IdentityType { get; set; }
        /// <summary>
        /// 客户端类型
        /// </summary>
        public LoginClientType LoginClientType { get; set; }
        /// <summary>
        /// 获取或设置 登录Id(每次登录该Id自动生成)
        /// </summary>
        public string LoginId { get; set; }
    }
}
