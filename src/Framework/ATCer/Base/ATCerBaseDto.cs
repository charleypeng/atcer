// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.Base
{
    /// <summary>
    /// BaseDto基类
    /// </summary>
    public abstract class ATCerBaseDto:ATCerBaseDto<int>
    {

    }

    /// <summary>
    /// BaseDto基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ATCerBaseDto<TKey>: BaseDto<TKey>, IBaseDto<TKey>
    {
        
    }

    /// <summary>
    /// BaseDto基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ATCerPureDto<TKey>:IBaseDto<TKey>
    {
        /// <summary>
        /// 编号
        /// </summary>
        [DisplayName("编号")]
        public TKey Id { get; set; }
    }

    /// <summary>
    /// BaseDto基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IBaseDto<TKey>
    {
        /// <summary>
        /// 编号
        /// </summary>
        [DisplayName("编号")]
        public TKey Id { get; set; }
    }
}
