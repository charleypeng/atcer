// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Furion.EventBus;
using ATCer.Base;
using ATCer.Base.Domains;
using ATCer.EventBus;

namespace ATCer.SystemManager.Subscribes
{
    /// <summary>
    /// 功能点变化清除关联关系
    /// </summary>
    public class FunctionDeleteClearRelationSubscriber : IEventSubscriber, ISingleton
    {

        /// <summary>
        /// 功能点变化
        /// </summary>
        /// <param name="context"></param>
        [EventSubscribe(nameof(EventType.EntityOperate) + nameof(Function) + nameof(EntityOperateType.Delete))]
        [EventSubscribe(nameof(EventType.EntityOperate) + nameof(Function) + nameof(EntityOperateType.FakeDelete))]
        public async Task Delete(EventHandlerExecutingContext context)
        {
            IEventSource eventSource = context.Source;
            IRepository<ResourceFunction> resourceFunctionRepository = Db.GetRepository<ResourceFunction>();
            Guid id = (Guid)eventSource.Payload;
            await resourceFunctionRepository.DeleteNowAsync(resourceFunctionRepository.Where(x => id.Equals(x.FunctionId)));
        }

        /// <summary>
        /// 功能点变化
        /// </summary>
        /// <param name="context"></param>
        [EventSubscribe(nameof(EventType.EntityOperate) + nameof(Function) + nameof(EntityOperateType.Deletes))]
        [EventSubscribe(nameof(EventType.EntityOperate) + nameof(Function) + nameof(EntityOperateType.FakeDeletes))]
        public async Task Deletes(EventHandlerExecutingContext context)
        {
            IEventSource eventSource = context.Source;
            IRepository<ResourceFunction> resourceFunctionRepository = Db.GetRepository<ResourceFunction>();
            IEnumerable<Guid> ids = (IEnumerable<Guid>)eventSource.Payload;
            await resourceFunctionRepository.DeleteNowAsync(resourceFunctionRepository.Where(x => ids.Contains(x.FunctionId)));
        }

    }
}
