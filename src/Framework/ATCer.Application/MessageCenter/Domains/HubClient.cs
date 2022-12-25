// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight 2021  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATCer.Base;
using Furion;
using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ATCer.Base;

namespace ATCer.MessageCenter.Domains
{
    public class HubClient : GardenerEntityBase<int>, IEntityTypeBuilder<HubClient>
    {
        public void Configure(EntityTypeBuilder<HubClient> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasKey(x => x.Id);
        }
    }
}
