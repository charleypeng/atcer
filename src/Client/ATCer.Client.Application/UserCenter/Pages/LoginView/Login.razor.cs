// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using AntDesign;
using ATCer.Authorization.Dtos;
using ATCer.Client.Base;
using ATCer.UserCenter.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Threading.Tasks;

namespace ATCer.UserCenter.Client.Pages.LoginView
{
    public partial class Login
    {
        bool loading = false;
        bool autoLogin = true;
        private LoginInput loginInput = new LoginInput();
        [Inject]
        public MessageService MsgSvr { get; set; }
        [Inject]
        public IAccountService accountService { get; set; }
        [Inject]
        public NavigationManager Navigation { get; set; }
        [Inject]
        public IAuthenticationStateManager authenticationStateManager { get; set; }

        private ATCer.Client.Components.ImageVerifyCode _imageVerifyCode;

        private string returnUrl;

        protected override  async Task OnInitializedAsync()
        {
            var url = new Uri(Navigation.Uri);
            var query = url.Query;

            if (QueryHelpers.ParseQuery(query).TryGetValue("returnUrl", out var value))
            {
                if (!value.Equals(Navigation.Uri))
                {
                    returnUrl = value;
                }
            }
            //已登录
            var user =  await authenticationStateManager.GetCurrentUser();
            if (user != null)
            {
                Navigation.NavigateTo(returnUrl ?? "/");
            }
           
        }
        
        private async Task OnLogin()
        {
            loading = true;
            var loginOutResult= await accountService.Login(loginInput);
            if (loginOutResult!=null)
            {
                await MsgSvr.Success($"登录成功",0.8);
                await authenticationStateManager.Login(loginOutResult, autoLogin);
                loading = false;
                Navigation.NavigateTo(returnUrl ?? "/");
            }
            else {
                await _imageVerifyCode.ReLoadVerifyCode();
                loading = false;
                await MsgSvr.Error($"登录失败");
                //await InvokeAsync(StateHasChanged);
            }
            
        }
    }
    
}
