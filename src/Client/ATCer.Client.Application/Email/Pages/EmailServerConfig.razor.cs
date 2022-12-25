// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Email.Dtos;

namespace ATCer.Email.Client.Pages
{
    public partial class EmailServerConfig : ListTableBase<EmailServerConfigDto, Guid, EmailServerConfigEdit>
    {
        /// <summary>
        /// 点击发送按钮
        /// </summary>
        /// <param name="roleDto"></param>
        protected async Task OnClickSend(Guid id)
        {
            OperationDialogSettings drawerSettings = GetOperationDialogSettings();
            OperationDialogInput<Guid> input = OperationDialogInput<Guid>.IsSelect(id);
            await OpenOperationDialogAsync<EmailServerConfigTest, OperationDialogInput<Guid>, OperationDialogOutput<Guid>>(localizer["发送"], input);
        }
    }
}
