// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2023  版权所有
// -----------------------------------------------------------------------------

namespace ATCer.Application.DataCenter.Builders.MetData;

public class HumiTempBuilder:IndexCreator<HUMITEMP, string>
{
    public HumiTempBuilder(ILogger<HumiTempBuilder> logger,
        IConfiguration configuration) : base(logger, configuration, IndexNames.MetData_HUMITEMP)
    {
        
    }
}