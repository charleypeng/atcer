// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.DataCenter.Dtos.MetDatDtos
{
    /// <summary>
    /// 大气压力
    /// </summary>
    public class PressDto:BaseMetDto
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
