// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Client.Base;
using ATCer.SysTimer.Dtos;
using ATCer.SysTimer.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATCer.SysTimer.Client.Pages
{
    public partial class SysTimerEdit :EditOperationDialogBase<SysTimerDto, int>
    {

        [Inject]
        private ISysTimerService sysTimerService { get; set; }

        /// <summary>
        /// 本地方法
        /// </summary>
        private IEnumerable<TaskMethodInfo> localJobs=new List<TaskMethodInfo>();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            localJobs = await sysTimerService.GetLocalJobs();
        }
    }
}
