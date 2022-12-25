// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.LTFATCenter.Domains;
using Furion.DatabaseAccessor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.LTFATCenter.Impl.Services
{
    [ApiDescriptionSettings("LTFATServices")]
    public class SADProcedureService:ATCer.ServiceBase<SADProcedure, SADProcedure, int>
    {
        public SADProcedureService(IRepository<SADProcedure> repository):base(repository)
        {

        }
    }
}
