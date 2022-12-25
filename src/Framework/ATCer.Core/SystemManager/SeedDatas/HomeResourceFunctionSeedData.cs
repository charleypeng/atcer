// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using ATCer.Base.Domains;
using Microsoft.EntityFrameworkCore;

namespace ATCer.SystemManager.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class HomeResourceFunctionSeedData : IEntitySeedData<ResourceFunction>
    {


        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<ResourceFunction> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[]{
                new ResourceFunction() { ResourceId = Guid.Parse("371b335b-29e5-4846-b6de-78c9cc691717"),FunctionId = Guid.Parse("8be6d20e-686c-4259-8eeb-3ec2b18739c3"),CreatedTime = DateTimeOffset.Parse("2022-08-12 13:54:54"),},
         };
        }
    }

}
