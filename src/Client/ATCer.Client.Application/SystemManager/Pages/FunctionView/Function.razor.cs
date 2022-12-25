// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Client.Components;
using ATCer.SystemManager.Dtos;
using ATCer.SystemManager.Services;
using Microsoft.AspNetCore.Components;

namespace ATCer.SystemManager.Client.Pages.FunctionView
{
    public partial class Function : ListTableBase<FunctionDto, Guid, FunctionEdit>
    {
        [Inject]
        public IFunctionService functionService { get; set; }

        // <summary>
        /// 点击启用审计按钮
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isLocked"></param>
        private async Task OnChangeEnableAudit(FunctionDto model, bool enableAudit)
        {
            var result = await functionService.EnableAudit(model.Id, enableAudit);
            if (!result)
            {
                model.EnableAudit = !enableAudit;
                messageService.Error((enableAudit ? "启用" : "禁用") + "失败");
            }
        }

        /// <summary>
        /// 点击导入按钮
        /// </summary>
        /// <returns></returns>
        private async Task OnImportClick()
        {
            var setting = base.GetOperationDialogSettings();
            setting.Width = 1000;
            await OpenOperationDialogAsync<FunctionImport, int, bool>(localizer["导入"], 0, async r =>
            {
                await ReLoadTable();

            }, setting);
        }

    }
}
