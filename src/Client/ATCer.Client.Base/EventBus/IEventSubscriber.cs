// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System.Threading.Tasks;

namespace ATCer.Client.Base
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public interface IEventSubscriber
    {

        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="e"></param>
        Task CallBack(object e);
    }
}
