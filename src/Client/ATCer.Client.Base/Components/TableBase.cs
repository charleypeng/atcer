// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using AntDesign;
using ATCer.Client.Base.Constants;
using ATCer.Client.Base.Services;
using Mapster;
using Microsoft.AspNetCore.Components;

namespace ATCer.Client.Components
{
    /// <summary>
    /// table基类
    /// </summary>
    public abstract class TableBase<TDto, TKey> : ReuseTabsPageBase where TDto : BaseDto<TKey>, new()
    {
        /// <summary>
        /// table引用
        /// </summary>
        protected ITable _table;

        /// <summary>
        /// 数据集合
        /// </summary>
        protected IEnumerable<TDto> _datas;

        /// <summary>
        /// 选择的行
        /// </summary>
        protected IEnumerable<TDto> _selectedRows;

        /// <summary>
        /// table加载中控制
        /// </summary>
        protected bool _tableIsLoading = false;

        /// <summary>
        /// 多选删除按钮加载中控制
        /// </summary>
        protected bool _deletesBtnLoading = false;

        /// <summary>
        /// 导出数据加载中绑定数据
        /// </summary>
        protected bool _exportDataLoading = false;

        /// <summary>
        /// 服务
        /// </summary>
        [Inject]
        protected IServiceBase<TDto, TKey> _service { get; set; }

        /// <summary>
        /// 本地化
        /// </summary>
        [Inject]
        protected IClientLocalizer localizer { get; set; }

        /// <summary>
        /// 操作对话框
        /// </summary>
        [Inject]
        protected IOperationDialogService operationDialogService { get; set; }

        /// <summary>
        /// 消息提示服务
        /// </summary>
        [Inject]
        protected MessageService messageService { get; set; }


        /// <summary>
        /// 获取操作会话配置
        /// </summary>
        /// <returns></returns>
        protected virtual OperationDialogSettings GetOperationDialogSettings()
        {
            OperationDialogSettings dialogSettings = new OperationDialogSettings();
            ClientConstant.DefaultOperationDialogSettings.Adapt(dialogSettings);
            SetOperationDialogSettings(dialogSettings);
            return dialogSettings;
        }

        /// <summary>
        /// 设置操作会话配置
        /// </summary>
        /// <param name="dialogSettings"></param>
        protected virtual void SetOperationDialogSettings(OperationDialogSettings dialogSettings)
        {
            //set OperationDialogSettings
        }


        /// <summary>
        /// 打开操作对话框
        /// </summary>
        /// <typeparam name="TOperationDialog"></typeparam>
        /// <typeparam name="TDialogInput"></typeparam>
        /// <typeparam name="TDialogOutput"></typeparam>
        /// <param name="title"></param>
        /// <param name="input"></param>
        /// <param name="onClose"></param>
        /// <param name="operationDialogSettings"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        protected async Task OpenOperationDialogAsync<TOperationDialog, TDialogInput, TDialogOutput>(string title, TDialogInput input, Func<TDialogOutput, Task> onClose = null, OperationDialogSettings operationDialogSettings = null, int? width = null) where TOperationDialog : FeedbackComponent<TDialogInput, TDialogOutput>
        {
            OperationDialogSettings settings = operationDialogSettings ?? GetOperationDialogSettings();
            if (width.HasValue)
            {
                settings.Width = width.Value;
            }
            await operationDialogService.OpenAsync<TOperationDialog, TDialogInput, TDialogOutput>(title, input, onClose, settings);
        }


        /// <summary>
        /// 点击锁定按钮
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isLocked"></param>
        protected virtual async Task OnChangeIsLocked(TDto model, bool isLocked)
        {
            var result = await _service.Lock(model.Id, isLocked);
            if (!result)
            {
                model.IsLocked = !isLocked;
                string msg = isLocked ? localizer["锁定"] : localizer["解锁"];

                await messageService.Error($"{msg} {localizer["失败"]}");
            }
        }
    }
}
