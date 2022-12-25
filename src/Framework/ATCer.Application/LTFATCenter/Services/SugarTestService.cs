// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.LTFATCenter.Dtos;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.LTFATCenter.Impl.Services
{
    public class SugarTestService: ATCer.ServiceBase<FlightPlan, FlightPlanDto, int, ATCerSlaveDbContextLocator>
    {
        public SugarTestService(IRepository<FlightPlan, ATCerSlaveDbContextLocator> repository):base(repository)
        {

        }
    }
}
