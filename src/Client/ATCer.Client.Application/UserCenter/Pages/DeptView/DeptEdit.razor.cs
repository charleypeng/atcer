// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Client.Base;
using ATCer.UserCenter.Dtos;
using ATCer.UserCenter.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATCer.UserCenter.Client.Pages.DeptView
{
    public partial class DeptEdit : EditOperationDialogBase<DeptDto, int>
    {
        [Inject]
        IDeptService deptService { get; set; }

        //部门树
        List<DeptDto> deptDatas = new List<DeptDto>();
        /// <summary>
        /// 父级部门编号
        /// </summary>
        private string ParentDeptId
        {
            get { return _editModel.ParentId?.ToString(); }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _editModel.ParentId = int.Parse(value);
                }
                else
                {
                    _editModel.ParentId = null;
                }

            }
        }
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            _isLoading = true;
            //父级
            deptDatas = await deptService.GetTree();
            await base.OnInitializedAsync();
            OperationDialogInput<int> editInput = this.Options;
            if (editInput.Type.Equals(DrawerInputType.Add))
            {
                _editModel.ParentId = editInput.Id==0?null: editInput.Id;
            }
            _isLoading = false;
        }

    }
}
