// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using ATCer.DataCenter.Dtos.MetDatDtos;
using ATCer.DataCenter.Services.MetData;

namespace ATCer.Client.Application.DataCenter.Services.MetData
{
    [ScopedService]
    public class PressService : ClientServiceBase<PressDto, string>, IPressService
    {
        public PressService(IApiCaller apiCaller) :base(apiCaller, "datacenter/met/press")
        {

        }
    }
}
