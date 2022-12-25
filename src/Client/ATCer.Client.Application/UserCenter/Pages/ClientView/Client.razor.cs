// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Client.Components;
using ATCer.UserCenter.Dtos;
using System;
using System.Threading.Tasks;

namespace ATCer.UserCenter.Client.Pages.ClientView
{
    public partial class Client : ListTableBase<ClientDto, Guid, ClientEdit>
    {
        /// <summary>
        /// 点击展示关联接口
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task OnShowFunctionClick(ClientDto model)
        {
            await OpenOperationDialogAsync<ClientFunctionEdit, ClientFunctionEditOption, bool>(
            $"{localizer["绑定接口"]}-[{model.Name}]",
            new ClientFunctionEditOption
            {
                Id = model.Id,
                Type = 0,
                Name = model.Name
            },
            width: 1200
            );
        }
    }
}
