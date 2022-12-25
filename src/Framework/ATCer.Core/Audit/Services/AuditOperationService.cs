// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using ATCer.Audit.Dtos;
using ATCer.EntityFramwork.Audit.Domains;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATCer.Audit.Services
{
    /// <summary>
    /// 审计操作服务
    /// </summary>
    [ApiDescriptionSettings("SystemBaseServices")]
    public class AuditOperationService : ServiceBase<AuditOperation, AuditOperationDto, Guid>, IAuditOperationService
    {
        private readonly IRepository<AuditEntity> _auditEntityRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="auditEntityRepository"></param>
        public AuditOperationService(IRepository<AuditOperation> repository, IRepository<AuditEntity> auditEntityRepository) : base(repository)
        {
            _auditEntityRepository = auditEntityRepository;
        }
        
        /// <summary>
        /// 根据操作审计ID获取数据审计
        /// </summary>
        /// <remarks>
        /// 根据操作审计ID获取数据审计
        /// </remarks>
        /// <param name="operationId"></param>
        /// <returns></returns>
        public async Task<List<AuditEntityDto>> GetAuditEntity([ApiSeat(ApiSeats.ActionStart)] Guid operationId)
        {
            return await _auditEntityRepository
                .Include(x => x.AuditProperties)
                .Where(x => x.IsDeleted == false && x.OperationId==operationId).Select(x => x.Adapt<AuditEntityDto>()).ToListAsync();
        }
    }
}
