// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Client.Base;
using ATCer.UserCenter.Dtos;
using System;
using System.Threading.Tasks;

namespace ATCer.UserCenter.Client.Pages.ClientView
{
    public partial class ClientEdit : EditOperationDialogBase<ClientDto, Guid>
    {

        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (this.Options.Type.Equals(DrawerInputType.Add))
            { 
                 _editModel.Id = Guid.NewGuid();
                 _editModel.SecretKey = Guid.NewGuid().ToString();
            }
        }
    }
}
