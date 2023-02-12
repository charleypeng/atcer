// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Client.Base;
using Microsoft.AspNetCore.Components;

namespace ATCer.Client.Native
{
    public partial class App
    {
        [Inject]
        private ClientModuleContext moduleContext { get; set; }
        [Inject]
        public NavigationManager Navigation { get; set; }

        private void GoHome() 
        {
            Navigation.NavigateTo("/");
        }
    }
}
