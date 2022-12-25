// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Email.Dtos;

namespace ATCer.Email.Client.Pages
{
    public partial class EmailTemplate : ListTableBase<EmailTemplateDto, Guid, EmailTemplateEdit>
    {
        protected override void SetOperationDialogSettings(OperationDialogSettings dialogSettings)
        {
            dialogSettings.Width = 600;
        }
        /// <summary>
        /// 点击发送按钮
        /// </summary>
        /// <param name="roleDto"></param>
        protected async Task OnClickSend(Guid id)
        {
            OperationDialogInput<Guid> input = OperationDialogInput<Guid>.IsSelect(id);
            await OpenOperationDialogAsync<EmailTemplateTest, OperationDialogInput<Guid>, OperationDialogOutput<Guid>>(localizer["发送"], input);
        }
    }
}
