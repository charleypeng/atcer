// -----------------------------------------------------------------------------
//  ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Mapster;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Furion;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Furion.DynamicApiController;

namespace ATCer.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntitySource"></typeparam>
    /// <typeparam name="TEntityDestination"></typeparam>
    /// <typeparam name="TSourceDbContextLocator"></typeparam>
    /// <typeparam name="TDestinationDbContextLocator"></typeparam>
    public abstract class DbSyncServiceBase<TEntitySource,TEntityDestination,TSourceDbContextLocator,TDestinationDbContextLocator>:ISyncService, IDynamicApiController
        where TEntitySource:class, IPrivateEntity, new()
        where TEntityDestination: class, IPrivateEntity, new()
        where TSourceDbContextLocator:class, IDbContextLocator
        where TDestinationDbContextLocator:class, IDbContextLocator
    {
        /// <summary>
        /// Source repository
        /// </summary>
        protected readonly IRepository<TEntitySource, TSourceDbContextLocator> _srcRepo;
        /// <summary>
        /// Destination repository
        /// </summary>
        protected readonly IRepository<TEntityDestination, TDestinationDbContextLocator> _desRepo;

        protected CancellationTokenSource source;

        protected CancellationToken Token;

        protected readonly ILogger<TEntitySource> logger;
        /// <summary>
        /// 
        /// </summary>
        public DbSyncServiceBase()
        {
            logger = App.GetService<ILogger<TEntitySource>>();
            _srcRepo = Db.GetRepository<TEntitySource, TSourceDbContextLocator>();
            _desRepo = Db.GetRepository<TEntityDestination, TDestinationDbContextLocator>();
            source = new CancellationTokenSource();
            Token = source.Token;

            Token.Register(() => logger.LogInformation("任务已创建"));
            
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual event EventHandler EntityChanged;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        public virtual async Task Start()
        {
            await SyncNow();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual async Task Stop()
        {
            source.Cancel();
            logger.LogInformation("同步任务被取消");
        }

        /// <summary>
        /// 分页添加 减少内存消耗
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [NonAction]
        public virtual async Task SyncNow(int pageIndex = 1, int pageSize = 100000)
        {
            var entities = _srcRepo.AsQueryable();
            var totalCount = await entities.CountAsync(Token);
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            if (totalPages == 0)
                return;

            logger.LogInformation($"准备复制更新数据库，总共{totalPages}页");

            for (int i = 1; i <= totalPages; i++)
            {
                var items = await entities.Skip((i - 1) * pageSize).Take(pageSize).ToListAsync(Token);
                await _desRepo.Context.BulkInsertOrUpdateAsync(items.Adapt<IList<TEntityDestination>>());
                logger.LogInformation($"第{i}页复制完成");
            }

            logger.LogInformation($"数据库更新完成");
        }
        [NonAction]
        public virtual async Task<ATCer.Base.MyPagedList<TEntitySource>> GetPage(int pageIndex = 1, int pageSize = 10000)
        {
            var pageResult = _srcRepo.AsQueryable();

            var result = await pageResult.ToPageAsync(pageIndex, pageSize);

            return result;
        }
    }

    
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntitySource"></typeparam>
    /// <typeparam name="TEntityDestination"></typeparam>
    public abstract class DbSyncServiceBase<TEntitySource, TEntityDestination> : DbSyncServiceBase<TEntitySource, TEntityDestination, MasterDbContextLocator, MasterDbContextLocator>
        where TEntitySource : class, IPrivateEntity, new()
        where TEntityDestination : class, IPrivateEntity, new()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntitySource"></typeparam>
    /// <typeparam name="TEntityDestination"></typeparam>
    /// <typeparam name="TDestinationDbContextLocator"></typeparam>
    public abstract class DbSyncServiceBase<TEntitySource, TEntityDestination, TDestinationDbContextLocator> : DbSyncServiceBase<TEntitySource, TEntityDestination, MasterDbContextLocator, TDestinationDbContextLocator>
        where TEntitySource : class, IPrivateEntity, new()
        where TEntityDestination : class, IPrivateEntity, new()
        where TDestinationDbContextLocator : class, IDbContextLocator
    {
        public DbSyncServiceBase()
        {
        }
    }

    public abstract class MSDbSyncServiceBase<TEntitySource, TDbContextLocator, TSlaveDbContextLocator> : ISyncService
        where TEntitySource : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
        where TSlaveDbContextLocator : class, IDbContextLocator
    {
        public event EventHandler EntityChanged;
        protected readonly IMSRepository<TDbContextLocator, TSlaveDbContextLocator> _mSRepository;
        protected CancellationTokenSource source;

        public MSDbSyncServiceBase(IMSRepository<TDbContextLocator, TSlaveDbContextLocator> mSRepository)
        {
            _mSRepository = mSRepository;
            source = new CancellationTokenSource();
        }
        public virtual async Task SyncNow(int pageIndex = 1, int pageSize = 10000)
        {
            var entities = _mSRepository.Master<TEntitySource>().AsQueryable();
            var totalCount = await entities.CountAsync(source.Token);
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            if (totalPages == 0)
                return;

            var logger = App.GetService<ILogger<TEntitySource>>();

            logger.LogInformation($"准备复制更新数据库，总共{totalPages}页");

            for (int i = 1; i <= totalPages; i++)
            {
                //var items = await entities.Skip((i - 1) * pageSize).Take(pageSize).ToListAsync(source.Token);
                //await _mSRepository.Slave1<TEntitySource>(). BulkInsertOrUpdateAsync(items.Adapt<IList<TEntitySource>>());
                //logger.LogInformation($"第{i}页复制完成");
            }

            logger.LogInformation($"数据库更新完成");
        }

        public Task Start()
        {
            throw new NotImplementedException();
        }

        public Task Stop()
        {
            throw new NotImplementedException();
        }
    }
}