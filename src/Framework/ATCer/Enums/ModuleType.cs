// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------
using System.ComponentModel;

namespace ATCer.Enums
{
    /// <summary>
    /// 模块类型
    /// </summary>
    public enum ModuleType:int
    {
        /// <summary>
        /// 根节点
        /// </summary>
        [Description("根节点")]
        Root =0,
        /// <summary>
        /// 模块组
        /// </summary>
        [Description("模块组")]
        Group = 1000,
        /// <summary>
        /// 菜单
        /// </summary>
        [Description("菜单")]
        Menu = 1500,
        /// <summary>
        /// BUTTON
        /// </summary>
        [Description("按钮")]
        Button = 2000
    }
}
