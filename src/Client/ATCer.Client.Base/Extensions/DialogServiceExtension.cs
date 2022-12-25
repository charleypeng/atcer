// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using AntDesign;
using System.Threading.Tasks;

namespace ATCer.Client.Base
{
    /// <summary>
    /// 弹框服务扩展
    /// </summary>
    public static class DialogServiceExtension
    {

        /// <summary>
        /// 关闭并返回参数
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="feedback"></param>
        /// <param name="result"></param>
        /// <remarks>
        /// ModalRef close 方法不支持传递参数，使用Ok方法。
        /// </remarks>
        /// <returns></returns>
        public static async Task CloseAsync<TResult>(this IFeedbackRef feedback, TResult result)
        {
            if (feedback is ModalRef modalRef)
            {
                var mRef = (modalRef as ModalRef<TResult>);
                //确定后的操作，放到关闭后执行
                if (mRef.OnClose != null)
                {
                    mRef.OnClose = async () =>
                    {
                        await mRef.OnOk?.Invoke(result);
                    };
                }
                await mRef.CloseAsync();
            }
            else if (feedback is DrawerRef drawerRef)
            {
                await (drawerRef as DrawerRef<TResult>)!.CloseAsync(result);
            }

        }
    }
}
