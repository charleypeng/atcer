// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.Client.Base
{
    public class OperationDialogOutput<TKey>
    {
        public bool Succeeded { get; set; }

        public TKey Id { get; set; }

        public DrawerOutputType Type { get; set; }
        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static OperationDialogOutput<TKey> Succeed(TKey id)
        {
            return new OperationDialogOutput<TKey>() {Id= id ,Succeeded=true,Type=DrawerOutputType.Succeeded};
        }
        /// <summary>
        /// 失败
        /// </summary>
        /// <returns></returns>
        public static OperationDialogOutput<TKey> Fail()
        {
            return new OperationDialogOutput<TKey>() { Succeeded=false, Type = DrawerOutputType.Failed };
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <returns></returns>
        public static OperationDialogOutput<TKey> Cancel()
        {
            return new OperationDialogOutput<TKey>() { Succeeded = false, Type = DrawerOutputType.Canceled };
        }
    }
    /// <summary>
    /// 抽屉返回结果类型
    /// </summary>
    public enum DrawerOutputType
    {
        /// <summary>
        /// 成功
        /// </summary>
        Succeeded=0,
        /// <summary>
        /// 失败
        /// </summary>
        Failed=1,
        /// <summary>
        /// 取消
        /// </summary>
        Canceled=2
    }
}
