//using Furion.DatabaseAccessor;
//using Mapster;
//using SqlSugar;
//using SqlSugar.Extensions;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using ATCer.Base.Extentions;
//using ATCer.Base;
//using Furion;
//using ATCer.Common;
//using System.Linq.Expressions;
//using Furion.DynamicApiController;

//namespace ATCer.Base
//{
//    /// <summary>
//    /// 
//    /// </summary>
//    /// <typeparam name="TEntity"></typeparam>
//    /// <typeparam name="TEntityDto"></typeparam>
//    /// <typeparam name="TKey"></typeparam>
//    public abstract class BaseEntityService<TEntity, TEntityDto, TKey> : IServiceBase<TEntityDto, TKey>, IDynamicApiController where TEntity : class, new() where TEntityDto : class, new()
//    {
//        /// <summary>
//        /// 
//        /// </summary>
//        protected readonly ISqlSugarClient db;
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="db"></param>
//        public BaseEntityService(ISqlSugarClient db)
//        {
//            this.db = db;
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        public async Task<bool> Delete(TKey id)
//        {
//            var result = await db.Deleteable<TEntity>().In(id).ExecuteCommandAsync();
//            return result == 1 ? true : false;
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="ids"></param>
//        /// <returns></returns>
//        public async Task<bool> Deletes(TKey[] ids)
//        {
//            var result = await db.Deleteable<TEntity>().In(ids).ExecuteCommandAsync();
//            return result == ids.Count() ? true : false;
//        }

//        public async Task<bool> FakeDelete(TKey id)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<bool> FakeDeletes(TKey[] ids)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<string> GenerateSeedData(ATCer.Base.PageRequest request)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<TEntityDto> Get(TKey id)
//        {
//            var result = await db.Queryable<TEntity>().InSingleAsync(id);
//            return result.Adapt<TEntityDto>();
//        }

//        public async Task<List<TEntityDto>> GetAll()
//        {
//            var result = await db.Queryable<TEntity>().ToListAsync();
//            return result.Adapt<List<TEntityDto>>();
//        }

//        public async Task<List<TEntityDto>> GetAllUsable()
//        {
//            var result = await db.Queryable<TEntity>().ToListAsync();
//            return result.Adapt<List<TEntityDto>>();
//        }

//        public async Task<ATCer.Base.PagedList<TEntityDto>> GetPage(int pageIndex = 1, int pageSize = 10)
//        {
//            //var queryable = db.Queryable<TEntity>();
//            //var result = await queryable.ToSugarPageAsync();
//            //return result.Adapt<ATCer.Base.PagedList<TEntityDto>>();
//            throw new NotImplementedException();
//        }

//        public async Task<TEntityDto> Insert(TEntityDto input)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<bool> Lock(TKey id, bool islocked = true)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<ATCer.Base.PagedList<TEntityDto>> Search(ATCer.Base.PageRequest request)
//        {
//            //IDynamicFilterService filterService = App.GetService<IDynamicFilterService>();
//            //if (typeof(TEntity).ExistsProperty(nameof(GardenerEntityBase.IsDeleted)))
//            //{
//            //    FilterGroup defaultFilterGroup = new FilterGroup();
//            //    defaultFilterGroup.AddRule(new FilterRule(nameof(GardenerEntityBase.IsDeleted), false, FilterOperate.Equal));
//            //    request.FilterGroups.Add(defaultFilterGroup);
//            //}
//            //Expression<Func<TEntity, bool>> expression = filterService.GetExpression<TEntity>(request.FilterGroups);

//            //var queryable = db.Queryable<TEntity>().Where(expression);
//            //var pg = await queryable
//            //            .Select(x => x.Adapt<TEntityDto>())
//            //            .ToPageListAsync(request.PageIndex, request.PageSize);
//            //var pgList = PagedList
//            throw new NotImplementedException();
//        }

//        public async Task<bool> Update(TEntityDto input)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<string> Export(PageRequest request)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
