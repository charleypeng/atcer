using ATCer.HRCenter.Dtos;
using ATCer.HRCenter.Enums;
using ATCer.Common;

namespace ATCer.HRCenter.Client.Pages
{
    public partial class SectorAdmin: ListTableBase<SectorDto, int, SectorEdit>
    {
        public readonly static TableFilter<PositionRole>[] FunctionPositionRoleFilters = EnumHelper.EnumToList<PositionRole>().Select(x => { return new TableFilter<PositionRole>() { Text = x.ToString(), Value = x }; }).ToArray();
        public readonly static TableFilter<ATCDepartment>[] FunctionATCDepartmentFilters = EnumHelper.EnumToList<ATCDepartment>().Select(x => { return new TableFilter<ATCDepartment>() { Text = x.ToString(), Value = x }; }).ToArray();
    }
}
