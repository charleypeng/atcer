// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.HRCenter.Domains;
using ATCer.HRCenter.Enums;

namespace ATCer.HRCenter.SeedDatas;

/// <summary>
/// Sector种子数据
/// </summary>
public class SectorSeedData : IEntitySeedData<Sector>
{
    /// <summary>
    /// Seed data
    /// </summary>
    /// <param name="dbContext"></param>
    /// <param name="dbContextLocator"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public IEnumerable<Sector> HasData(DbContext dbContext, Type dbContextLocator)
    {
        return new Sector[]
        {
                new Sector{Id=1, Code="TWR", Name="塔台管制席", Multiplier=1.05, Department = ATCDepartment.TWR, Position = PositionRole.InCommand,CreatedTime = DateTimeOffset.Now}
        };
    }
}
