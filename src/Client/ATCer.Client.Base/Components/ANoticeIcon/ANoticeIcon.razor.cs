using AntDesign;
using Microsoft.AspNetCore.Components;

namespace ATCer.Client.Components
{
    public partial class ANoticeIcon : AntDomComponentBase
    {
        private readonly Trigger[] _trigger = { Trigger.Hover };

        [Parameter] public bool Visible { get; set; }
        [Parameter] public int Count { get; set; }
        [Parameter] public string ClearText { get; set; }
        [Parameter] public string ViewMoreText { get; set; }
        [Parameter] public EventCallback<string> OnClear { get; set; }
        [Parameter] public EventCallback<string> OnViewMore { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            SetClassMap();
        }

        protected void SetClassMap()
        {
            ClassMapper
                .Clear()
                .Add("noticeButton")
                .If("opened", () => Visible);
        }
    }
}