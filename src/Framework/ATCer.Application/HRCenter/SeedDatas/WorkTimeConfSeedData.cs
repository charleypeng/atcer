// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.HRCenter.Domains;
using ATCer.HRCenter.Options;

namespace ATCer.HRCenter.SeedDatas;

/// <summary>
/// 
/// </summary>
public class WorkTimeConfSeedData : IEntitySeedData<WorkTimeConf>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dbContext"></param>
    /// <param name="dbContextLocator"></param>
    /// <returns></returns>
    public IEnumerable<WorkTimeConf> HasData(DbContext dbContext, Type dbContextLocator)
    {
        TimeCalculateOptions options = new TimeCalculateOptions();

        var result = new WorkTimeConf[]
            {
                new WorkTimeConf
                {
                    Id = 1,
                    CreatedTime = DateTime.Parse("2023-1-1"),
                    DaySpan = options.DaySpan,
                    NightSpan = options.NightSpan,
                    ChangeLog = "初版",
                    CreatorId = "1",
                    Version = 1,
                    NightShiftMultiplier = 1.5,
                    CreatorIdentityType = Authentication.Enums.IdentityType.User
                }
            };

        return result;
    }
}
