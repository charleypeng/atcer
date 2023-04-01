// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.EntityFramwork.Event
{
    /// <summary>
    /// 保存数据更改事件
    /// </summary>
    public class ATCerDbContextSavedChangesEvent
    {
        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }
    }
}
