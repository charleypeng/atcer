// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Microsoft.JSInterop;

namespace ATCer.Client.Interops
{
    public class ATCerInterop
    {
        public ATCerInterop(IJSRuntime jSRuntime)
        {
            Js = jSRuntime;
        }
        private IJSRuntime Js { get; set; }
        private Lazy<Task<IJSObjectReference>> moduleTask => new(() => Js.InvokeAsync<IJSObjectReference>("import", "./_content/ATCer.Client.Base/atcer.js").AsTask());
        public async Task MoveButtonTest()
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("setTitle", "atcer_rank_list");

            var dd = await setTitle("这点JFK大厦离开");
        }

        public async Task MoveButton(string elementId)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("moveButton", elementId);
        }

        public async ValueTask<string> setTitle(string msg)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<string>("setTitle", msg);
        }
    }
}
