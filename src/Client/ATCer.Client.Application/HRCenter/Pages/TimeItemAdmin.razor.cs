// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.HRCenter.Dtos;
using ATCer.HRCenter.Enums;
using ATCer.Common;

namespace ATCer.HRCenter.Client.Pages
{
    public partial class TimeItemAdmin: ListTableBase<TimeItemDto, long, TimeItemEdit>
    {
        public readonly static TableFilter<ATCLoginType>[] LoginFilters = EnumHelper.EnumToList<ATCLoginType>().Select(x => { return new TableFilter<ATCLoginType>() { Text = x.ToString(), Value = x }; }).ToArray();
        public readonly static TableFilter<ATCLoginType>[] LogoutFilters = EnumHelper.EnumToList<ATCLoginType>().Select(x => { return new TableFilter<ATCLoginType>() { Text = x.ToString(), Value = x }; }).ToArray();
        public readonly static TableFilter<ControllerRole>[] ControllerRoleFilters = EnumHelper.EnumToList<ControllerRole>().Select(x => { return new TableFilter<ControllerRole>() { Text = x.ToString(), Value = x }; }).ToArray();
    }
}
