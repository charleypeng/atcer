// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.FanoutMq
{
    public delegate Task AsyncEventHandler<in TEvent>(object sender, TEvent @event) where TEvent : EventArgs;

    internal static class AsyncEventHandlerExtensions
    {
        public static async Task InvokeAsync<TEvent>(this AsyncEventHandler<TEvent> eventHandler, object sender, TEvent @event) where TEvent : EventArgs
        {
            if (eventHandler != null)
            {
                foreach (AsyncEventHandler<TEvent> handlerInstance in eventHandler.GetInvocationList())
                {
                    await handlerInstance(sender, @event).ConfigureAwait(false);
                }
            }
        }
    }
}
