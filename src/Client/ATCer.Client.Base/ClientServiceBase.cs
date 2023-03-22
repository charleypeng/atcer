// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATCer.Client.Base
{
    /// <summary>
    /// 客户端服务基类
    /// </summary>
    /// <typeparam name="T">Dto</typeparam>
    public abstract class ClientServiceBase<T> : ClientServiceBase<T, int> where T : class, new()
    {
        /// <summary>
        /// 客户端服务基类
        /// </summary>
        /// <param name="apiCaller"></param>
        /// <param name="controller"></param>
        protected ClientServiceBase(IApiCaller apiCaller, string controller) : base(apiCaller, controller)
        {
        }
    }
    /// <summary>
    /// 客户端服务基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="Tkey"></typeparam>
    public abstract class ClientServiceBase<T,Tkey> : IServiceBase<T, Tkey> where T : class, new()
    {
        public readonly string controller;
        public readonly IApiCaller apiCaller;
        protected ClientServiceBase(IApiCaller apiCaller, string controller)
        {
            this.apiCaller = apiCaller;
            this.controller = controller;
        }

        public virtual Task<bool> Delete(Tkey id)
        {
            return apiCaller.DeleteAsync<bool>($"{controller}/{id}");
        }

        public virtual Task<bool> Deletes(Tkey[] ids)
        {
            return apiCaller.PostAsync<Tkey[], bool>($"{controller}/deletes", ids);
        }

        public virtual async Task<bool> FakeDelete(Tkey id)
        {
            
            var data = await apiCaller.DeleteAsync<bool>($"{controller}/fake-delete/{id}");
            return data;
        }

        public virtual Task<bool> FakeDeletes(Tkey[] ids)
        {
            return apiCaller.PostAsync<Tkey[], bool>($"{controller}/fake-deletes", ids);
        }

        public virtual Task<T> Get(Tkey id)
        {
            return apiCaller.GetAsync<T>($"{controller}/{id}");
        }

        public virtual Task<List<T>> GetAll()
        {
            return apiCaller.GetAsync<List<T>>($"{controller}/all");
        }

        public virtual Task<MyPagedList<T>> GetPage(int pageIndex = 1, int pageSize = 10)
        {
            return apiCaller.GetAsync<MyPagedList<T>>($"{controller}/page/{pageIndex}/{pageSize}");
        }

        public virtual Task<T> Insert(T input)
        {
            return apiCaller.PostAsync<T, T>(controller, request: input);
        }

        public virtual Task<bool> Update(T input)
        {
            return apiCaller.PutAsync<T, bool>(controller, request: input);
        }
        public virtual Task<bool> Lock(Tkey id, bool islocked = true)
        {
            return apiCaller.PutAsync<object, bool>($"{controller}/{id}/lock/{islocked}");
        }

        public virtual Task<List<T>> GetAllUsable()
        {
            return apiCaller.GetAsync<List<T>>($"{controller}/all-usable");
        }

        public virtual async Task<MyPagedList<T>> Search(MyPageRequest request)
        {
            var data = await apiCaller.PostAsync<MyPageRequest, MyPagedList<T>>($"{controller}/search",request);
            return data;
        }

        public virtual Task<string> GenerateSeedData(MyPageRequest request)
        {
            return apiCaller.PostAsync<MyPageRequest, string>($"{controller}/generate-seed-data", request);
        }

        public virtual async Task<string> Export(MyPageRequest request)
        {
            return await apiCaller.PostAsync<MyPageRequest, string>($"{controller}/export", request);
        }
    }
}
