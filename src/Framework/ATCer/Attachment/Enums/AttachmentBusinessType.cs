// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System.ComponentModel;

namespace ATCer.Attachment.Enums
{
    /// <summary>
    /// 附件业务类型类型
    /// </summary>
    public enum AttachmentBusinessType
    {
        /// <summary>
        /// 头像
        /// </summary>
        [Description("头像")]
        Avatar,
        /// <summary>
        /// 订单
        /// </summary>
        [Description("订单")]
        Order,
        /// <summary>
        /// 聊天
        /// </summary>
        [Description("聊天")]
        Chat,
        /// <summary>
        /// 执勤小时数据
        /// </summary>
        [Description("执勤小时数据")]
        TimeItem
    }
}
