// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2023  版权所有
// -----------------------------------------------------------------------------

using ATCer.DataCenter;
using ATCer.DataCenter.Domains;
using ATCer.ElasticSearch.Core;
using Microsoft.Extensions.Configuration;

namespace ATCer.Application.DataCenter.Builders.MetData;

public class PRESSBuilder:IndexCreator<PRESS, string>
{
    public PRESSBuilder(ILogger<PRESSBuilder> logger,
        IConfiguration configuration) : base(logger, configuration, IndexNames.Names[0])
    {
        
    }
}