// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion;
using Furion.DatabaseAccessor;
using ATCer.Audit.Dtos;
using ATCer.Base;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ATCer.EntityFramwork.Audit.Domains;

namespace ATCer.Audit.Services
{
    /// <summary>
    /// 审计数据服务
    /// </summary>
    [ApiDescriptionSettings("SystemBaseServices")]
    public class AuditEntityService : ServiceBase<AuditEntity, AuditEntityDto, Guid>, IAuditEntityService
    {
        private readonly IRepository<AuditEntity> _auditRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public AuditEntityService(IRepository<AuditEntity> repository) : base(repository)
        {
            this._auditRepository = repository;
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <remarks>
        /// 搜索数据
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public override async Task<MyPagedList<AuditEntityDto>> Search(MyPageRequest request)
        {
            IDynamicFilterService filterService = App.GetService<IDynamicFilterService>();

            Expression<Func<AuditEntity, bool>> expression = filterService.GetExpression<AuditEntity>(request.FilterGroups);

            IQueryable<AuditEntity> queryable = _auditRepository
                .Include(x=>x.AuditProperties)
                .Where(expression);
            return await queryable
                .OrderConditions(request.OrderConditions)
                .Select(x => x.Adapt<AuditEntityDto>())
                .ToPageAsync(request.PageIndex, request.PageSize);
        }
    }
}
