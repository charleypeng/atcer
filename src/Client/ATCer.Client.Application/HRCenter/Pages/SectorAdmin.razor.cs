using ATCer.HRCenter.Dtos;
using ATCer.HRCenter.Enums;
using ATCer.Common;
using ATCer.Enums;

namespace ATCer.HRCenter.Client.Pages
{
    public partial class SectorAdmin: ListTableBase<SectorDto, int, SectorEdit>
    {
        public readonly static TableFilter<PositionRole>[] FunctionPositionRoleFilters = EnumHelper.EnumToList<PositionRole>().Select(x => { return new TableFilter<PositionRole>() { Text = EnumHelper.GetEnumDescription(x), Value = x }; }).ToArray();
        public readonly static TableFilter<ATCDepartment>[] FunctionATCDepartmentFilters = EnumHelper.EnumToList<ATCDepartment>().Select(x => { return new TableFilter<ATCDepartment>() { Text = EnumHelper.GetEnumDescription(x).ToString(), Value = x }; }).ToArray();
    }
}
