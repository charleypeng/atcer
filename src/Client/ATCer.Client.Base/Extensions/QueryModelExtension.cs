// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using AntDesign;
using AntDesign.TableModels;
using ATCer.Base;
using Mapster;
using System.Collections.Generic;
using System.Linq;

namespace ATCer.Client.Base
{
    public static class QueryModelExtension
    {
        /// <summary>
        /// QueryModel 转换成api的数据结构
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        public static List<FilterGroup> GetFilterGroups(this ITable table)
        {
            QueryModel queryModel = table.GetQueryModel();

            List<FilterGroup> filterGroups = new List<FilterGroup>();
            if (queryModel != null)
            {
                foreach (ITableFilterModel model in queryModel.FilterModel)
                {
                    FilterGroup fieldGroup = new FilterGroup();
                    filterGroups.Add(fieldGroup);
                    IList<TableFilter> tableFilters = model.Filters;
                    foreach (TableFilter filter in tableFilters)
                    {
                        FilterOperate operate = FilterOperate.Equal;
                        switch (filter.FilterCompareOperator)
                        {
                            case TableFilterCompareOperator.Equals: operate = FilterOperate.Equal; break;
                            case TableFilterCompareOperator.IsNull: operate = FilterOperate.Equal; break;
                            case TableFilterCompareOperator.NotEquals: operate = FilterOperate.NotEqual; break;
                            case TableFilterCompareOperator.IsNotNull: operate = FilterOperate.NotEqual; break;
                            case TableFilterCompareOperator.Contains: operate = FilterOperate.Contains; break;
                            case TableFilterCompareOperator.NotContains: operate = FilterOperate.NotContains; break;
                            case TableFilterCompareOperator.StartsWith: operate = FilterOperate.StartsWith; break;
                            case TableFilterCompareOperator.EndsWith: operate = FilterOperate.EndsWith; break;
                            case TableFilterCompareOperator.LessThan: operate = FilterOperate.Less; break;
                            case TableFilterCompareOperator.LessThanOrEquals: operate = FilterOperate.LessOrEqual; break;
                            case TableFilterCompareOperator.GreaterThan: operate = FilterOperate.Greater; break;
                            case TableFilterCompareOperator.GreaterThanOrEquals: operate = FilterOperate.GreaterOrEqual; break;
                        }
                        FilterRule rule = new FilterRule(model.FieldName, filter.Value, operate);
                        rule.Condition = filter.FilterCondition.Equals(TableFilterCondition.And) ? FilterCondition.And : FilterCondition.Or;

                        fieldGroup.Rules.Add(rule);
                    }
                }
            }
            return filterGroups;
        }

        /// <summary>
        /// QueryModel 转换成api的数据结构
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        public static List<ListSortDirection> GetOrderConditions(this ITable table)
        {
            QueryModel queryModel = table.GetQueryModel();
            List<ListSortDirection> sortDirections = new List<ListSortDirection>();
            if (queryModel ==null || queryModel.SortModel == null || queryModel.SortModel.Count == 0)
            {
                return sortDirections;
            }
            sortDirections = queryModel.
                SortModel
                .Where(x=>!string.IsNullOrEmpty(x.Sort))
                .Select(x => x.Adapt<ListSortDirection>()).ToList();
            return sortDirections;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        public static MyPageRequest GetPageRequest(this ITable table) 
        {
            QueryModel queryModel = table.GetQueryModel();
            MyPageRequest request = new MyPageRequest();
            if (queryModel != null) 
            {
                request.PageIndex = queryModel.PageIndex;
                request.PageSize = queryModel.PageSize;
                request.FilterGroups = table.GetFilterGroups();
                request.OrderConditions = table.GetOrderConditions();
            }
            return request;
        }
    }
}
