using System;
using ATCer.HRCenter.Dtos;
using ATCer.HRCenter.Enums;
namespace ATCer.HRCenter.Extensions;

public static class ImportUserATCInfoExtension
{
    public static IList<UserATCInfoDto> AdaptUserATCInfo(this IEnumerable<ImportUserAtoInfoDto> infoDtos)
    {
        if (infoDtos == null || infoDtos.Count() == 0)
            return default(IList<UserATCInfoDto>)!;

        var lst = new List<UserATCInfoDto>();
        foreach (var item in infoDtos)
        {
            var info = new UserATCInfoDto();

            info.ATCName = item.UserName;
            info.ATCLevel = item.UserLevel;
            info.Department = item.Department;
            info.Role = item.HirachyName.ToConrollerRole();
            info.CreatedTime = DateTime.Now;
            lst.Add(info);
        }
        return lst;
    }
}

