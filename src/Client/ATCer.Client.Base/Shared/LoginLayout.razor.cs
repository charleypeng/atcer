// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Client.Base.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace ATCer.Client.Base.Shared
{
    public partial class LoginLayout
    {
        /// <summary>
        /// 系统配置服务
        /// </summary>
        [Inject]
        private ISystemConfigService SystemConfigService { get; set; }
        [Inject]
        private IJsTool JsTool { get; set; }
        [Inject]
        private UserOptions userOptions { get; set; }

        private SystemConfig systemConfig;

        private string computedBgColor
        {
            get
            {
                if(userOptions.DarkMode == true)
                {
                    return "black";
                }
                else
                {
                    return "";
                }
            }
        }

        private string computedFontColor
        {
            get
            {
                if (userOptions.DarkMode == true)
                {
                    return "white";
                }
                else
                {
                    return "black";
                }
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            
        }

        protected async override Task OnInitializedAsync()
        {
            systemConfig = SystemConfigService.GetSystemConfig();
            await JsTool.Document.SetTitle(systemConfig.SystemName);
            await base.OnInitializedAsync();
        }

        protected override bool ShouldRender()
        {
            return base.ShouldRender();
        }
    }
}
