// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------
using ATCer.Attributes;
using System.ComponentModel;

namespace ATCer.Base.Enums
{
    /// <summary>
    /// 权限类型
    /// </summary>
    public enum ResourceType : int
    {
        /// <summary>
        /// 根节点
        /// </summary>
        [Description("根节点")]
        [IgnoreOnConvertToMap]
        Root = 0,
        /// <summary>
        /// 菜单
        /// </summary>
        [Description("菜单")]
        Menu = 1000,
        /// <summary>
        /// 操作
        /// </summary>
        [Description("操作")]
        Action = 2000,
        /// <summary>
        /// 视图
        /// </summary>
        [Description("视图")]
        View = 3000,
    }
}
