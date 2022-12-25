// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using AntDesign.ProLayout;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;

#nullable disable
namespace ATCer.Client.UserCenter.Pages.Account.Settings
{
    public partial class ThemeView:AntDomComponentBase
    {
        [Parameter]
        public bool ColorWeak { get; set; }
        [Parameter]
        public string Theme { get; set; } = "light";
        [Parameter]
        public string PrimaryColor { get; set; } = "daybreak";

        [Inject]
        private UserOptions _userOptions { get; set; }
        [Inject]
        private IOptions<ProSettings> proSettings { get; set; }
        [Inject]
        private MessageService Message { get; set; }

        private ElementReference _linkRef;
        private string PrefixCls { get; } = "ant-pro";
        private string BaseClassName => $"{PrefixCls}-setting";
        private string _url;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
           
            ColorWeak = proSettings.Value.ColorWeak;
            Theme = proSettings.Value.NavTheme;
        }

        private async Task UpdateTheme(string theme)
        {
            proSettings.Value.NavTheme = theme;
            await UpdateStyle();
        }

        private async Task UpdateColor(string color)
        {
            PrimaryColor = proSettings.Value.PrimaryColor = color;
            await UpdateStyle();
        }

        private async Task UpdateStyle()
        {
            _ = Message.Loading(new MessageConfig
            {
                Content = "正在更新界面设置...",
                Duration = 1
            });

            var color = proSettings.Value.PrimaryColor;
            var theme = proSettings.Value.NavTheme;

            string fileName;
            if (theme == "realDark")
            {
                fileName = color == "daybreak" ? "dark" : $"dark-{color}";
                _userOptions.DarkMode = true;
            }
            else
            {
                fileName = color == "daybreak" ? null : color;
                _userOptions.DarkMode = false;
            }

            Console.WriteLine(new { color, theme });

            _url = fileName == null ? "" : $"/_content/AntDesign.ProLayout/theme/{fileName}.css";

            await JsInvokeAsync(JSInteropConstants.AddElementToBody, _linkRef);
        }
    }
}
