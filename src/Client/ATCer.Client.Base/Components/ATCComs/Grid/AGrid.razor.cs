// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System.Threading.Tasks;

namespace ATCer.Client.Components;

public partial class AGrid
{
    /// <summary>
    /// 是否可以拖拽
    /// </summary>
    [Parameter] 
    public bool Draggable { get; set; }
    /// <summary>
    /// 拖拽内容(AGrid)
    /// </summary>
    [Parameter] 
    public RenderFragment ChildContent { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
    }
}
