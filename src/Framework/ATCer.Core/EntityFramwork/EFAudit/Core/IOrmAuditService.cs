// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.EntityFramwork.Audit.Domains;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATCer.EntityFramwork.Audit.Core
{
    /// <summary>
    /// Orm审计服务接口
    /// </summary>
    public interface IOrmAuditService
    {
        /// <summary>
        /// 数据保存前
        /// </summary>
        /// <param name="entitys"></param>
        public void SavingChangesEvent(IEnumerable<EntityEntry> entitys);
        /// <summary>
        /// 数据保存后
        /// </summary>
        public Task SavedChangesEvent();

        /// <summary>
        /// 保存操作审计
        /// </summary>
        /// <param name="auditOperation"></param>
        public Task SaveAuditOperation(AuditOperation auditOperation);

    }
}
