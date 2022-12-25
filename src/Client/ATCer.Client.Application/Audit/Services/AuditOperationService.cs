// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ATCer.Audit.Dtos;
using ATCer.Audit.Services;
using ATCer.Client.Base;

namespace ATCer.Audit.Client.Services
{
    [ScopedService]
    public class AuditOperationService : ClientServiceBase<AuditOperationDto, Guid>, IAuditOperationService
    {
        public AuditOperationService(IApiCaller apiCaller) : base(apiCaller, "audit-operation")
        {
        }

        public async Task<List<AuditEntityDto>> GetAuditEntity(Guid operationId)
        {
            return await apiCaller.GetAsync<List<AuditEntityDto>>($"{controller}/{operationId}/audit-entity");
        }
    }
}
