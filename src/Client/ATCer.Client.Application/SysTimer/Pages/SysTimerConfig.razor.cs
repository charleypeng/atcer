// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using AntDesign;
using ATCer.Client.Base;
using ATCer.Client.Components;
using ATCer.Common;
using ATCer.SysTimer.Dtos;
using ATCer.SysTimer.Enums;
using ATCer.SysTimer.Services;
using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ATCer.SysTimer.Client.Pages
{
    public partial class SysTimerConfig : ListTableBase<SysTimerDto, int, SysTimerEdit>
    {
        [Inject]
        private ISysTimerService _systimerService { get; set; }

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected async Task OnStartExecute(SysTimerDto model)
        {
            string title = TimerStatus.Running.Equals(model.TimerStatus) ? "关闭" : "开启";
            var resultStop = await confirmService.YesNo(localizer[title], localizer["确认要执行该操作吗？"]);
            if (resultStop == ConfirmResult.Yes)
            {
                switch (model.TimerStatus)
                {
                    case TimerStatus.Running:
                        await _systimerService.Stop(model.JobName);
                        break;
                    case TimerStatus.Stopped:
                        await _systimerService.Start(model.JobName);
                        break;
                    default:
                        await _systimerService.Start(model.JobName);
                        break;
                }
                Thread.Sleep(1000);
                await ReLoadTable(false);
            }
            
        }

        public readonly static TableFilter<ExecuteType>[] FunctionRequestTypeFilters = EnumHelper.EnumToList<ExecuteType>().Select(x => { return new TableFilter<ExecuteType>() { Text = x.ToString(), Value = x }; }).ToArray();
        public readonly static TableFilter<TimerStatus>[] FunctionTimerStatusFilters = EnumHelper.EnumToList<TimerStatus>().Select(x => { return new TableFilter<TimerStatus>() { Text = x.ToString(), Value = x }; }).ToArray();
    }
}
