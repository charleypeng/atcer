// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Client.Base;
using ATCer.Client.Components;
using ATCer.UserCenter.Dtos;
using ATCer.UserCenter.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATCer.UserCenter.Client.Pages.DeptView
{
    public partial class Dept : TreeTableBase<DeptDto, int, DeptEdit>
    {

        protected override OperationDialogSettings GetOperationDialogSettings()
        {
            OperationDialogSettings dialogSettings = base.GetOperationDialogSettings();
            dialogSettings.Width = 1000;
            return dialogSettings;
        }

        [Inject]
        public IDeptService deptService { get; set; }

        protected override ICollection<DeptDto> GetChildren(DeptDto dto)
        {
            return dto.Children;
        }


        protected override int GetParentKey(DeptDto dto)
        {
            return dto.ParentId.HasValue ? dto.ParentId.Value : 0;
        }

        protected override async Task<List<DeptDto>> GetTree()
        {
            return await deptService.GetTree(true);
        }

        protected override void SetChildren(DeptDto dto, ICollection<DeptDto> children)
        {
            dto.Children = children;
        }


        protected override ICollection<DeptDto> SortChildren(ICollection<DeptDto> children)
        {
            return children.OrderBy(x=>x.Order).ToList();
        }
    }
}
