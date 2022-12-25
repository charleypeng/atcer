// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Enums;

namespace ATCer.Base
{
    /// <summary>
    /// 搜索排序
    /// </summary>
    public class ListSortDirection
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 0 asc 
        /// 1 desc
        /// </summary>
        public ListSortType SortType { get; set; } = ListSortType.Desc;
    }
    
}
