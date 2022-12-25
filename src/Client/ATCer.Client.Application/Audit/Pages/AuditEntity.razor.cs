// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Audit.Dtos;
using ATCer.Client.Components;

namespace ATCer.Audit.Client.Pages
{
    public partial class AuditEntity : ListTableBase<AuditEntityDto, Guid>
    {
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="auditEntity"></param>
        /// <returns></returns>
        private async Task OnDetailClick(AuditEntityDto auditEntity)
        {
          await OpenOperationDialogAsync<AuditEntityDetailDrawer, ICollection<AuditEntityDto>, bool>(localizer["字段变更详情"], new[] { auditEntity }, width: 960);
        }
    }
}
