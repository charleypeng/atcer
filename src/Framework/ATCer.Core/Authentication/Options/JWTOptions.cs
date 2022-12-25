// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion.ConfigurableOptions;
using ATCer.Authentication.Enums;
using System.Collections.Generic;

namespace ATCer.Authentication.Options
{
    /// <summary>
    /// JWT 配置信息
    /// </summary>
    public class JWTOptions : IConfigurableOptions
    {
        /// <summary>
        /// 设置字典
        /// </summary>
        public Dictionary<IdentityType, JWTSettingsOptions> Settings { get; set; }
    }
}
