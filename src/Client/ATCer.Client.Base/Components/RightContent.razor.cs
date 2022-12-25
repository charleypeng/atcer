// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using AntDesign;
using System.Globalization;
using ATCer.UserCenter.Dtos;
using AntDesign.ProLayout;
using ATCer.Client.Base.Constants;
using System.Collections;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using ATCer.Client.Components;

namespace ATCer.Client.Components
{
    public partial class RightContent
    {
        private UserDto _currentUser;
        public string[] Locales { get; set; } = { "zh-CN" };
        [Inject] 
        protected NavigationManager NavigationManager { get; set; }
        [Inject] 
        protected MessageService MessageService { get; set; }
        [Inject]
        protected IAuthenticationStateManager authenticationStateManager { get; set; }

        [Inject]
        private IJsTool JsTool { get; set; }
        [Inject]
        private IClientLocalizer localizer{ get; set; }
        [Inject]
        private UserOptions _userOptions { get; set; }
        #region Notification
        private int _count = 20;
        private ICollection<ANoticeIconData> _notifications = new List<ANoticeIconData>();
        private ICollection<ANoticeIconData> _messages = new List<ANoticeIconData>();
        private ICollection<ANoticeIconData> _events = new List<ANoticeIconData>();
        private async Task HandleClear() { }
        private async Task HandleViewMore() { }
        #endregion
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            SetClassMap();
            _currentUser =await authenticationStateManager.GetCurrentUser();
        }

        protected void SetClassMap()
        {
            ClassMapper
                .Clear()
                .Add("right");
        }
        public AvatarMenuItem[] AvatarMenuItems { get; set; } = new AvatarMenuItem[]
        {
            new() { Key = "center", IconType = "user", Option = "个人中心" },
            new() { Key = "setting", IconType = "setting", Option ="个人设置" },
            new() { IsDivider = true },
            new() { Key = "logout", IconType = "logout", Option = "退出登录" }
        };
        public async Task HandleSelectUser(MenuItem item)
        {
            switch (item.Key)
            {
                case "center":
                    NavigationManager.NavigateTo($"/account/center/{_currentUser.UserName}");
                    break;
                case "setting":
                    NavigationManager.NavigateTo($"/account/settings");
                    break;
                case "logout":
                    await authenticationStateManager.Logout();
                    //NavigationManager.NavigateTo("/auth/login?returnUrl="+ Uri.EscapeDataString(NavigationManager.Uri));
                    //await InvokeAsync(StateHasChanged);
                    //移除所有tab
                    ClientNavTabControl.RemoveAllNavTabPage();
                    break;
            }
        }

        public async Task HandleSelectLang(MenuItem item)
        {
            string name = item.Key;
            if (CultureInfo.CurrentCulture.Name != name)
            {
                await JsTool.SessionStorage.SetAsync(ClientConstant.BlazorCultureKey, name);
                NavigationManager.NavigateTo(NavigationManager.Uri, forceLoad: true);
            }
            
        }
    }
}