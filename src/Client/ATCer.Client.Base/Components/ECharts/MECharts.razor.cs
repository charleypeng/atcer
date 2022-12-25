// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace ATCer.Client.Components;
#nullable disable
public partial class MECharts : AntDomComponentBase, IDisposable, IAsyncDisposable, IHandleEvent, IMEChart
{
    [Inject] UserOptions userOptions { get; set; }
    [CascadingParameter]
    protected IErrorHandler ErrorHandler { get; set; }
    [Parameter]
    public StringNumber Width { get; set; } = 600;

    [Parameter]
    public StringNumber Height { get; set; } = 400;

    [Parameter]
    public StringNumber MinWidth { get; set; }

    [Parameter]
    public StringNumber MinHeight { get; set; }

    [Parameter]
    public StringNumber MaxWidth { get; set; }

    [Parameter]
    public StringNumber MaxHeight { get; set; }

    [Parameter]
    public Action<EChartsInitOptions> InitOptions { get; set; }

    [Parameter]
    public object Option { get; set; } = new { };

    [Parameter]
    public bool Light { get; set; }

    [Parameter]
    public bool Dark { get; set; }

    [Parameter]
    public string Theme { get; set; } = "light";

    [CascadingParameter(Name = "IsDark")]
    public bool CascadingIsDark { get; set; }

    private EChartsInitOptions DefaultInitOptions { get; set; } = new();

    private IJSObjectReference _echarts;
    private bool _isEChartsDisposed = false;
    private object _prevOption;

    public string ComputedTheme
    {
        get
        {
            if (!Theme.Equals("light"))
            {
                return Theme;
            }

            if (Dark)
            {
                return "dark";
            }

            return null;
        }
    }

    protected override string GenerateStyle()
    {
        Style = $"width:{Width};height:{Height};"; //min-width:{MinWidth};max-width:{MaxWidth};min-height:{MinHeight};max-height:{MaxHeight}";
        return Style;
    }

    protected override void OnParametersSet()
    {
        InitOptions?.Invoke(DefaultInitOptions);

        DefaultInitOptions.Locale ??= "zhCN".ToUpperInvariant();
    }
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        userOptions.PropertyChanged += UserOptions_PropertyChanged;
    }
    protected override Task OnFirstAfterRenderAsync()
    {
        GenerateStyle();
        return base.OnFirstAfterRenderAsync();
    }
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (IsDisposed)
        {
            return;
        }

        if (firstRender)
        {
            Dark = userOptions.DarkMode;
            GenerateStyle();
            _echarts = await Js.InvokeAsync<IJSObjectReference>("import", "./_content/ATCer.Client.Base/js/echarts-helper.js");
        }

        if (firstRender || _isEChartsDisposed)
        {
            _isEChartsDisposed = false;
            await InitECharts();
        }

        if (_prevOption != Option)
        {
            _prevOption = Option;

            if (firstRender) return;

            await ResetOption();
        }
    }

    private async void UserOptions_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if(e.PropertyName == nameof(userOptions.DarkMode))
        {
            if(Dark != userOptions.DarkMode)
            {
                Dark = userOptions.DarkMode;
                await DisposeECharts();
                await InitECharts();
            }
        }
    }

    private async Task InitECharts()
    {
        if (_echarts is null) return;
        DefaultInitOptions.Width = Width;
        DefaultInitOptions.Height = Height;
        await _echarts.InvokeVoidAsync("init", Ref.GetSelector(), ComputedTheme, DefaultInitOptions, Option);
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public async Task DisposeECharts()
    {
        if (_echarts is null) return;
        try
        {
            await _echarts.InvokeVoidAsync("dispose", Ref.GetSelector());
        }
        catch (Exception)
        {
            Console.WriteLine("echarts already disposed");
        }
        
        _isEChartsDisposed = true;
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public async Task ResetOption()
    {
        if (_echarts is null) return;
        await _echarts.InvokeVoidAsync("setOption", Ref.GetSelector(), Option);
    }

    public async Task SetOption(object options, bool notMerge = false, bool lazyUpdate = false)
    {
        if (_echarts is null) return;
        await _echarts.InvokeVoidAsync("updateOption", Ref.GetSelector(), options,notMerge, lazyUpdate);
    }

    public async ValueTask DisposeAsync()
    {
        try
        {
            await DisposeECharts();
            //dettach
            userOptions.PropertyChanged -= UserOptions_PropertyChanged;
            if (_echarts is not null)
            {
                await _echarts.DisposeAsync();
            }
        }
        catch
        {
            // ignored
        }
    }

    public Task HandleEventAsync(EventCallbackWorkItem callback, object arg)
    {
        var task = callback.InvokeAsync(arg);
        var shouldAwaitTask = task.Status != TaskStatus.RanToCompletion &&
                              task.Status != TaskStatus.Canceled;

        if (AfterHandleEventShouldRender())
        {
            StateHasChanged();
        }

        return shouldAwaitTask
            ? CallStateHasChangedOnAsyncCompletion(task)
            : Task.CompletedTask;
    }

    protected virtual bool AfterHandleEventShouldRender()
    {
        return true;
    }

    private async Task CallStateHasChangedOnAsyncCompletion(Task task)
    {
        try
        {
            await task;
        }
        catch (Exception ex) // avoiding exception filters for AOT runtime support
        {
            // Ignore exceptions from task cancellations, but don't bother issuing a state change.
            if (task.IsCanceled)
            {
                return;
            }

            if (ErrorHandler != null)
            {
                await ErrorHandler.HandleExceptionAsync(ex);
            }
            else
            {
                throw;
            }
        }

        if (AfterHandleEventShouldRender())
        {
            StateHasChanged();
        }
    }

    ~MECharts()
    {
        Dispose(false);
    }
}
