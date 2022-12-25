// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using AntDesign;
using System.Threading.Tasks;
using System;

namespace ATCer.Client.Base.Services
{
    /// <summary>
    /// 操作对话框服务
    /// </summary>
    public interface IOperationDialogService
    {
        /// <summary>
        /// 打开
        /// </summary>
        /// <typeparam name="TOperationDialog"></typeparam>
        /// <typeparam name="TDialogInput"></typeparam>
        /// <typeparam name="TDialogOutput"></typeparam>
        /// <param name="drawerService"></param>
        /// <param name="modalService"></param>
        /// <param name="title"></param>
        /// <param name="input"></param>
        /// <param name="onClose"></param>
        /// <param name="dialogSettings"></param>
        /// <remarks>
        /// 可以是抽屉，可以是弹框
        /// </remarks>
        /// <returns></returns>
        public Task OpenAsync<TOperationDialog, TDialogInput, TDialogOutput>(string title, TDialogInput input, Func<TDialogOutput, Task> onClose = null, OperationDialogSettings dialogSettings = null) where TOperationDialog : FeedbackComponent<TDialogInput, TDialogOutput>;

        /// <summary>
        /// 打开
        /// </summary>
        /// <typeparam name="TOperationDialog"></typeparam>
        /// <typeparam name="TDialogInput"></typeparam>
        /// <param name="drawerService"></param>
        /// <param name="modalService"></param>
        /// <param name="title"></param>
        /// <param name="input"></param>
        /// <param name="onClose"></param>
        /// <param name="dialogSettings"></param>
        /// <remarks>
        /// 可以是抽屉，可以是弹框
        /// </remarks>
        /// <returns></returns>
        public Task OpenAsync<TOperationDialog, TDialogInput>(string title, TDialogInput input, Func<Task> onClose = null, OperationDialogSettings dialogSettings = null) where TOperationDialog : FeedbackComponent<TDialogInput,bool>;

    }
}
