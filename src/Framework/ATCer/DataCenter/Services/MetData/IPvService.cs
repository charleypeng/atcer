// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Base;
using ATCer.DataCenter.Dtos.MetDatDtos;

namespace ATCer.DataCenter.Services.MetData
{
    /// <summary>
    /// 大气压力数据服务
    /// </summary>
    public interface IPvService : IServiceBase<PvDto, string>
    {
    }
}
