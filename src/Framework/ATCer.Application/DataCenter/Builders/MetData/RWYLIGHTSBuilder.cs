// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using ATCer.DataCenter.Domains.MH4016_9;
using ATCer.ElasticSearch.Core;
using Microsoft.Extensions.Configuration;

namespace ATCer.Application.DataCenter.Builders.MetData
{
    public class RWYLIGHTSBuilder:IndexCreator<RWYLIGHTS, string>
    {
        public RWYLIGHTSBuilder(ILogger<RWYLIGHTSBuilder> logger,
        IConfiguration configuration) : base(logger, configuration, IndexNames.MetData_RwyLights)
        { }
    }
}
