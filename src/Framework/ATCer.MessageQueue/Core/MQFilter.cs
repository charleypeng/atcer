// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using DotNetCore.CAP.Filter;

namespace ATCer.MessageQueue
{
    /// <summary>
    /// MQ过滤器
    /// </summary>
    public class MQFilter : SubscribeFilter
    {
        /// <summary>
        /// 订阅前执行
        /// </summary>
        /// <param name="context"></param>
        public override Task OnSubscribeExecutingAsync(ExecutingContext context)
        {
            return base.OnSubscribeExecutingAsync(context);
        }
    }
}
