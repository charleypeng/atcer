// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.UserCenter.Dtos;
using Microsoft.AspNetCore.Components;

namespace ATCer.Client.UserCenter.Pages.Account.Settings
{
    public partial class BaseView
    {
        private UserDto _currentUser = new UserDto();
        private bool isLoading = true;
        [Inject]
        private IAuthenticationStateManager _authenticationStateManager { get; set; }

        private async Task actionUploadAvatar()
        {

        }
        private void HandleFinish()
        {
        }

        protected override async Task OnInitializedAsync()
        {
            isLoading = true;
            await base.OnInitializedAsync();
            _currentUser = await _authenticationStateManager.GetCurrentUser();
            isLoading = false;
        }
    }
}
