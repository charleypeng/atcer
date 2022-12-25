// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Client.Base;
using ATCer.Client.Components;
using ATCer.UserCenter.Dtos;
using System;
using System.Threading.Tasks;

namespace ATCer.UserCenter.Client.Pages.RoleView
{
    public partial class Role : ListTableBase<RoleDto, int, RoleEdit>
    {

        /// <summary>
        /// 点击分配资源
        /// </summary>
        /// <returns></returns>
        private async Task OnEditRoleResourceClick(int id)
        {
            await OpenOperationDialogAsync<RoleResourceEdit, OperationDialogInput<int>, bool>(localizer["绑定资源"], OperationDialogInput<int>.IsEdit(id), width: 600);
        }


        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        private async Task OnDownloadClick()
        {
            await OpenOperationDialogAsync<RoleResourceDownload, string, bool>(
                localizer["种子数据"],
                      string.Empty,
                       width: 1300);
        }

    }
}
