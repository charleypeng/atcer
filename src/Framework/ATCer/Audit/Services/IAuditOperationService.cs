// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Audit.Dtos;
using ATCer.Base;

namespace ATCer.Audit.Services
{
    /// <summary>
    /// 审计操作服务
    /// </summary>
    public interface IAuditOperationService : IServiceBase<AuditOperationDto, Guid>
    {
        /// <summary>
        /// 根据操作审计ID获取数据审计数据
        /// </summary>
        /// <param name="operationId"></param>
        /// <returns></returns>
        Task<List<AuditEntityDto>> GetAuditEntity(Guid operationId);
    }
}
