// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using ATCer.DataCenter.Enums;
using ATCer.ElasticSearch.Interfaces;

namespace ATCer.DataCenter.Domains
{
    /// <summary>
    /// 大气压力
    /// </summary>
    public class PRESS:BaseMetDomain
    {
        /// <summary>
        /// 瞬时气压
        /// <para>单位:hPa</para>
        /// </summary>
        public MetTuple? PAINS { get; set; }
        /// <summary>
        /// 瞬时场面气压
        /// <para>单位：hPa</para>
        /// </summary>
        public MetTuple? QFEINS { get; set; }
        /// <summary>
        /// 瞬时场面气压
        /// <para>单位：hPa</para>
        /// </summary>
        public MetTuple? QFEM { get; set; }
        /// <summary>
        /// 瞬时场面气压
        /// <para>单位：hPa</para>
        /// </summary>
        public MetTuple? QFEX { get; set; }
        /// <summary>
        /// 瞬时场面气压
        /// <para>单位：hPa</para>
        /// </summary>
        public MetTuple? QFFINS { get; set; }
        /// <summary>
        /// 瞬时场面气压
        /// <para>单位：hPa</para>
        /// </summary>
        public MetTuple? QNHINS { get; set; }
        /// <summary>
        /// 瞬时场面气压
        /// <para>单位：hPa</para>
        /// </summary>
        public MetTuple? QFESYNOP { get; set; }
        /// <summary>
        /// 瞬时场面气压
        /// <para>单位：hPa</para>
        /// </summary>
        public MetTuple? QFESYNOPT { get; set; }
        /// <summary>
        /// 瞬时场面气压
        /// <para>单位：hPa</para>
        /// </summary>
        public MetTuple? QFESYSNOPT { get; set; }
        /// <summary>
        /// 瞬时场面气压
        /// <para>单位：hPa</para>
        /// </summary>
        public MetTuple? QFESYSNOP3H { get; set; }
        /// <summary>
        /// 瞬时场面气压
        /// <para>单位：hPa</para>
        /// </summary>
        public MetTuple? FL { get; set; }
        /// <summary>
        /// 瞬时场面气压
        /// <para>单位：hPa</para>
        /// </summary>
        public MetTuple? ALT { get; set; }
    }
}
