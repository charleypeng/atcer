// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using ATCer.Common;
using ATCer.Base;
using System.Linq.Expressions;

namespace ATCer
{
    /// <summary>
    /// 分部拓展类
    /// </summary>
    public static class RepositoryExtensions
    {
        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="repository"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        private static async Task FakeDeleteAsync<TEntity>(this IRepository<TEntity> repository, TEntity entity) where TEntity : class, IPrivateEntity, new()
        {
            if (entity != null && entity.SetPropertyValue(nameof(GardenerEntityBase.IsDeleted), true))
            {
                entity.SetPropertyValue(nameof(GardenerEntityBase.UpdatedTime), DateTimeOffset.Now);
                await repository.UpdateIncludeAsync(entity, new[] { nameof(GardenerEntityBase.IsDeleted), nameof(GardenerEntityBase.UpdatedTime) });
            }
        }
        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="repository"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        private static async Task FakeDeleteNowAsync<TEntity>(this IRepository<TEntity> repository, TEntity entity) where TEntity : class, IPrivateEntity, new()
        {
            if (entity != null && entity.SetPropertyValue(nameof(GardenerEntityBase.IsDeleted), true))
            {
                entity.SetPropertyValue(nameof(GardenerEntityBase.UpdatedTime), DateTimeOffset.Now);
                await repository.UpdateIncludeNowAsync(entity, new[] { nameof(GardenerEntityBase.IsDeleted), nameof(GardenerEntityBase.UpdatedTime) });
            }
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDbContextLocator"></typeparam>
        /// <param name="repository"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        private static async Task FakeDeleteAsync<TEntity, TDbContextLocator>(this IRepository<TEntity, TDbContextLocator> repository, TEntity entity) where TEntity : class, IPrivateEntity, new() where TDbContextLocator : class, IDbContextLocator
        {
            if (entity != null && entity.SetPropertyValue(nameof(GardenerEntityBase.IsDeleted), true))
            {
                entity.SetPropertyValue(nameof(GardenerEntityBase.UpdatedTime), DateTimeOffset.Now);
                await repository.UpdateIncludeAsync(entity, new[] { nameof(GardenerEntityBase.IsDeleted), nameof(GardenerEntityBase.UpdatedTime) });
            }
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDbContextLocator"></typeparam>
        /// <param name="repository"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        private static async Task FakeDeleteNowAsync<TEntity, TDbContextLocator>(this IRepository<TEntity, TDbContextLocator> repository, TEntity entity) where TEntity : class, IPrivateEntity, new() where TDbContextLocator : class, IDbContextLocator
        {
            if (entity != null && entity.SetPropertyValue(nameof(GardenerEntityBase.IsDeleted), true))
            {
                entity.SetPropertyValue(nameof(GardenerEntityBase.UpdatedTime), DateTimeOffset.Now);
                await repository.UpdateIncludeNowAsync(entity, new[] { nameof(GardenerEntityBase.IsDeleted), nameof(GardenerEntityBase.UpdatedTime) });
            }
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="repository"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task FakeDeleteByKeyAsync<TEntity, TKey>(this IRepository<TEntity> repository, TKey id) where TEntity : class, IPrivateEntity, new()
        {
            TEntity entity = await repository.FindAsync(id);
            await repository.FakeDeleteAsync(entity);
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="repository"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task FakeDeleteNowByKeyAsync<TEntity, TKey>(this IRepository<TEntity> repository, TKey id) where TEntity : class, IPrivateEntity, new()
        {
            TEntity entity = await repository.FindAsync(id);
            await repository.FakeDeleteNowAsync(entity);
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TDbContextLocator"></typeparam>
        /// <param name="repository"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task FakeDeleteByKeyAsync<TEntity, TKey, TDbContextLocator>(this IRepository<TEntity, TDbContextLocator> repository, TKey id) where TEntity : class, IPrivateEntity, new() where TDbContextLocator : class, IDbContextLocator
        {
            TEntity entity = await repository.FindAsync(id);
            await repository.FakeDeleteAsync(entity);
        }
        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TDbContextLocator"></typeparam>
        /// <param name="repository"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task FakeDeleteNowByKeyAsync<TEntity, TKey, TDbContextLocator>(this IRepository<TEntity, TDbContextLocator> repository, TKey id) where TEntity : class, IPrivateEntity, new() where TDbContextLocator : class, IDbContextLocator
        {
            TEntity entity = await repository.FindAsync(id);
            await repository.FakeDeleteNowAsync(entity);
        }

        /// <summary>
        /// 获取标准过滤可用查询
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="tracking"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> AsDefaultQuaryable<TEntity>(this IRepository<TEntity> entity, bool tracking = false)
            where TEntity : class, IPrivateEntity, IBaseEntity, new()
        {
            var et = entity.AsQueryable(tracking).Where(x => x.IsDeleted == false, x => x.IsDeleted == false);
            return et;
        }

        /// <summary>
        /// 获取标准过滤可用查询
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDbContextLocator"></typeparam>
        /// <param name="entity"></param>
        /// <param name="tracking"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> AsDefaultQuaryable<TEntity, TDbContextLocator>(this IRepository<TEntity, TDbContextLocator> entity, bool tracking = false)
            where TEntity : class, IPrivateEntity, IBaseEntity, new()
            where TDbContextLocator : class, IDbContextLocator
        {
            var et = entity.AsQueryable(tracking).Where(x => x.IsDeleted == false, x => x.IsDeleted == false);
            return et;
        }

        /// <summary>
        /// 返回一个过滤后的查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="quary"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static IQueryable<T> GWhere<T>(this IQueryable<T> quary, Expression<Func<T, bool>> expression) where T : ATCer.Base.IATCerEntity
        {
            return quary.Where(x => x.IsDeleted == false && x.IsLocked == false).Where(expression);
        }
    }
}
