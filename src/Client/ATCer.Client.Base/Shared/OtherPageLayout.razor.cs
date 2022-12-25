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
    public partial class OtherPageLayout
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

        private string bgColor = "#f0f2f5";
        private string titlColor = "black";
        protected async override Task OnInitializedAsync()
        {
            if (userOptions.DarkMode)
            {
                bgColor = "black";
                titlColor = "#f0f2f5";
            }
                
            else
            {
                bgColor = "#f0f2f5";
                titlColor = "black";
            }
                
            systemConfig = SystemConfigService.GetSystemConfig();
            await JsTool.Document.SetTitle(systemConfig.SystemName);
            await base.OnInitializedAsync();
        }
    }
}
