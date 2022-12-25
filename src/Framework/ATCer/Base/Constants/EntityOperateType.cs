// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.Base
{
    /// <summary>
    /// 实体操作类型
    /// </summary>
    public enum EntityOperateType
    {
        /// <summary>
        /// 删除
        /// </summary>
        Delete,
        /// <summary>
        /// 批量删除
        /// </summary>
        Deletes,
        /// <summary>
        /// 逻辑删除
        /// </summary>
        FakeDelete,
        /// <summary>
        /// 批量逻辑删除
        /// </summary>
        FakeDeletes,
        /// <summary>
        /// 添加
        /// </summary>
        Insert,
        /// <summary>
        /// 更新
        /// </summary>
        Update,
        /// <summary>
        /// 锁定
        /// </summary>
        Lock
    }
}
