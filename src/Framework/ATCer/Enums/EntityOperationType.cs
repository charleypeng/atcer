// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System.ComponentModel;

namespace ATCer.Enums
{
    /// <summary>
    /// 数据实体操作类型
    /// </summary>
    public enum EntityOperationType
    {
        /// <summary>
        /// 添加
        /// </summary>
        [Description("添加")]
        Add,

        /// <summary>
        /// 修改
        /// </summary>
        [Description("修改")]
        Update,

        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Delete
    }
}
