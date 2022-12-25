// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Audit.Dtos;
using ATCer.Audit.Services;
using ATCer.Client.Components;
using Microsoft.AspNetCore.Components;

namespace ATCer.Audit.Client.Pages
{
    public partial class AuditOperation : ListTableBase<AuditOperationDto, Guid>
    {
        [Inject]
        public IAuditOperationService auditOperationService { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="auditEntity"></param>
        /// <returns></returns>
        private async Task OnDetailClick(Guid id)
        {
            List<AuditEntityDto>  auditEntityDtos= await auditOperationService.GetAuditEntity(id);

            await OpenOperationDialogAsync<AuditEntityDetailDrawer, ICollection<AuditEntityDto>, bool>(localizer["详情"],auditEntityDtos, width: 960);
        }
    }
}
