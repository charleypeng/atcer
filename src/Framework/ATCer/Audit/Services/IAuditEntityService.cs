// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Audit.Dtos;
using ATCer.Base;
using System;

namespace ATCer.Audit.Services
{
    /// <summary>
    /// 审计数据服务接口
    /// </summary>
    public interface IAuditEntityService : IServiceBase<AuditEntityDto, Guid>
    {
        
    }
}
