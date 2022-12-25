// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using ATCer.Base;
using ATCer.Base.Domains;
using ATCer.SystemManager.Dtos;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web;
using MyHttpMethod = ATCer.Enums.MyHttpMethod;

namespace ATCer.SystemManager.Services
{
    /// <summary>
    /// 功能服务
    /// </summary>
    [ApiDescriptionSettings("SystemBaseServices")]
    public class FunctionService : ServiceBase<Function, FunctionDto, Guid>, IFunctionService
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public FunctionService(IRepository<Function> repository) : base(repository)
        {
        }

        /// <summary>
        /// 启用或禁用
        /// </summary>
        /// <remarks>
        /// 启用或禁用功能
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="enableAudit"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> EnableAudit([ApiSeat(ApiSeats.ActionStart)] Guid id,bool enableAudit = true)
        {
            var entity = await _repository.FindAsync(id);
            if (entity == null) return false;
            entity.EnableAudit = enableAudit;
            entity.UpdatedTime = DateTimeOffset.UtcNow;
            await _repository.UpdateIncludeAsync(entity, new[] { nameof(Function.EnableAudit), nameof(Function.UpdatedTime) });
            //发送通知
            await EntityEventNotityUtil.NotifyUpdateAsync(entity);
            return true;
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <remarks>
        /// 根据 HttpMethod 和 path 判断是否存在
        /// </remarks>
        /// <param name="method"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<bool> Exists(MyHttpMethod method, string path)
        {
            path = HttpUtility.UrlDecode(path);

            return await _repository.Where(x => x.Method.Equals(method) && x.Path.Equals(path),tracking:false).AnyAsync();
        }

        /// <summary>
        /// 根据key获取
        /// </summary>
        /// <remarks>
        /// 根据key获取 功能点
        /// </remarks>
        /// <param name="key">key</param>
        /// <returns></returns>
        public async Task<FunctionDto> GetByKey(string key)
        {
            Function function= await _repository.Where(x => x.Key.Equals(key), tracking: false).FirstOrDefaultAsync();
            return function?.Adapt<FunctionDto>();
        }
    }
}
