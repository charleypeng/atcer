// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Audit.Dtos;
using ATCer.Audit.Services;
using ATCer.Client.Base;
using System;

namespace ATCer.Audit.Client.Services
{
    /// <summary>
    /// 审计数据服务
    /// </summary>
    [ScopedService]
    public class AuditEntityService : ClientServiceBase<AuditEntityDto, Guid>, IAuditEntityService
    {
        public AuditEntityService(IApiCaller apiCaller) : base(apiCaller, "audit-entity")
        {
        }
    }
}
