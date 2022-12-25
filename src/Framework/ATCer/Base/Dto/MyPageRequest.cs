// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System.Collections.Generic;

namespace ATCer.Base
{
    /// <summary>
    /// 
    /// </summary>
    public class MyPageRequest
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; } = 1;
        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { get; set; } = 10;
        /// <summary>
        /// 排序集合
        /// </summary>
        public List<ListSortDirection> OrderConditions { get; set; } = new List<ListSortDirection>();
        /// <summary>
        /// 查询条件组
        /// </summary>
        public List<FilterGroup> FilterGroups { get; set; } = new List<FilterGroup>() ;

    }
}
