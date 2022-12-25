﻿// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;

namespace ATCer
{
    /// <summary>
    /// 主从数据库操作基类
    /// 继承此类即可实现基础方法
    /// 方法包括：CURD、获取全部、分页获取 
    /// </summary>
    /// <typeparam name="TEntity">主从数据实体类型</typeparam>
    /// <typeparam name="TEntityDto">数据实体对应DTO类型</typeparam>
    /// <typeparam name="TKey">数据实体主键类型</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <typeparam name="TSlaveDbContextLocator">从数据库上下文定位器</typeparam>
    public abstract class MSServiceBase<TEntity, TEntityDto, TKey, TDbContextLocator, TSlaveDbContextLocator> : ServiceBase<TEntity, TEntityDto, TKey, TDbContextLocator>
        where TEntity : class, IEntity<TDbContextLocator, TSlaveDbContextLocator>, new()
        where TEntityDto : class, new()
        where TDbContextLocator : class, IDbContextLocator
        where TSlaveDbContextLocator : class, IDbContextLocator
    {
        /// <summary>
        /// TEntity Repository
        /// </summary>
        public readonly IMSRepository<TDbContextLocator, TSlaveDbContextLocator> _msRepository;
        /// <summary>
        /// 继承此类即可实现基础方法
        /// 方法包括：CURD、获取全部、分页获取 
        /// </summary>
        /// <param name="repository"></param>
        protected MSServiceBase(IMSRepository<TDbContextLocator, TSlaveDbContextLocator> repository):base(repository.Master<TEntity>())
        {
            _msRepository = repository;
        }

        /// <summary>
        /// 获取可读仓库对象
        /// </summary>
        /// <returns></returns>
        protected override IPrivateReadableRepository<TEntity> GetReadableRepository()
        {
            return _msRepository.Slave1<TEntity>();
        }
    }

    /// <summary>
    /// 主从数据库操作基类
    /// 继承此类即可实现基础方法
    /// 方法包括：CURD、获取全部、分页获取 
    /// </summary>
    /// <typeparam name="TEntity">数据实体类型</typeparam>
    /// <typeparam name="TEntityDto">数据实体对应DTO类型</typeparam>
    /// <typeparam name="TSlaveDbContextLocator">从数据库上下文定位器</typeparam>
    public abstract class MSServiceBase<TEntity, TEntityDto, TSlaveDbContextLocator> : MSServiceBase<TEntity, TEntityDto, int, TSlaveDbContextLocator>
        where TEntity : class, IEntity<MasterDbContextLocator, TSlaveDbContextLocator>, new()
        where TEntityDto : class, new()
        where TSlaveDbContextLocator : class, IDbContextLocator
    {
        /// <summary>
        /// 继承此类即可实现基础方法
        /// 方法包括：CURD、获取全部、分页获取 
        /// </summary>
        /// <param name="repository"></param>
        protected MSServiceBase(IMSRepository<MasterDbContextLocator, TSlaveDbContextLocator> repository) : base(repository)
        {
        }
    }

    /// <summary>
    /// 继承此类即可实现基础方法
    /// 方法包括：CURD、获取全部、分页获取 
    /// </summary>
    /// <typeparam name="TEntity">数据实体类型</typeparam>
    /// <typeparam name="TEntityDto">数据实体对应DTO类型</typeparam>
    /// <typeparam name="TKey">数据实体主键</typeparam>
    /// <typeparam name="TSlaveDbContextLocator">从数据库上下文定位器</typeparam>
    public abstract class MSServiceBase<TEntity, TEntityDto, TKey, TSlaveDbContextLocator> : MSServiceBase<TEntity, TEntityDto, TKey, MasterDbContextLocator, TSlaveDbContextLocator>
        where TEntity : class, IEntity<MasterDbContextLocator, TSlaveDbContextLocator>, new()
        where TEntityDto : class, new()
        where TSlaveDbContextLocator : class, IDbContextLocator
    {
        /// <summary>
        /// 继承此类即可实现基础方法
        /// 方法包括：CURD、获取全部、分页获取 
        /// </summary>
        /// <param name="repository"></param>
        protected MSServiceBase(IMSRepository<MasterDbContextLocator, TSlaveDbContextLocator> repository) : base(repository)
        {
        }
    }
}
