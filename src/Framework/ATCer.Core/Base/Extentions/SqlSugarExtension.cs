//// -----------------------------------------------------------------------------
//// ATCer 全平台综合性空中交通管理系统
////  作者：彭磊
////  CopyRight(C) 2022  版权所有 
//// -----------------------------------------------------------------------------

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using SqlSugar;
//using ATCer.Base;
//using Furion.FriendlyException;
//using ATCer.Enums;

//namespace ATCer.Base.Extentions
//{
//    public static class SqlSugarExtension
//    {
//        /// <summary>
//        /// 分页拓展
//        /// </summary>
//        /// <typeparam name="TEntity"></typeparam>
//        /// <param name="entities"></param>
//        /// <param name="pageIndex"></param>
//        /// <param name="pageSize"></param>
//        /// <param name="cancellationToken"></param>
//        /// <returns></returns>
//        public static async Task<ATCer.Base.PagedList<TEntity>> ToSugarPageAsync<TEntity>(this ISugarQueryable<TEntity> entities, int pageIndex = 1, int pageSize = 20, CancellationToken cancellationToken = default)
//            where TEntity : class, new()
//        {
//            var totalCount = await entities.CountAsync();
//            var items = await entities.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
//            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

//            return new ATCer.Base.PagedList<TEntity>
//            {
//                PageIndex = pageIndex,
//                PageSize = pageSize,
//                Items = items,
//                TotalCount = totalCount,
//                TotalPages = totalPages,
//                HasNextPages = pageIndex < totalPages,
//                HasPrevPages = pageIndex - 1 > 0
//            };
//        }

//        public static IQueryable<T> OrderConditions<T>(this IQueryable<T> query, ListSortDirection[] orderConditions)
//        {
//            if (orderConditions == null || !orderConditions.Any()) return query;
//            var parameter = Expression.Parameter(typeof(T), "o");
//            for (var i = 0; i < orderConditions.Length; i++)
//            {
//                var orderinfo = orderConditions[i];
//                var t = typeof(T);
//                var property = t.GetProperty(orderinfo.FieldName);
//                if (property == null)
//                {
//                    throw Oops.Oh(ExceptionCode.FIELD_IN_TYPE_NOT_FOUND, orderinfo.FieldName, t.Name);
//                }
//                //创建一个访问属性的表达式
//                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
//                var orderByExp = Expression.Lambda(propertyAccess, parameter);
//                string OrderName = i > 0 ? "ThenBy" : "OrderBy";
//                OrderName = OrderName + (orderinfo.SortType.Equals(ListSortType.Desc) ? "Descending" : "");
//                MethodCallExpression resultExp = Expression.Call(typeof(Queryable), OrderName, new Type[] { typeof(T), property.PropertyType }, query.Expression, Expression.Quote(orderByExp));
//                query = query.Provider.CreateQuery<T>(resultExp);
//            }
//            return query;
//        }
//    }
//}
