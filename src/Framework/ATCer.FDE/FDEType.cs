using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.FDE
{
    /// <summary>
    /// 报文类型
    /// </summary>
    public enum FDEType
    {
        /// <summary>
        /// 飞行计划数据
        /// </summary>
        IFPL,
        /// <summary>
        /// 飞行计划删除数据
        /// </summary>
        IDEL,
        /// <summary>
        /// 飞行计划取消数据
        /// </summary>
        ICNL,
        /// <summary>
        /// 物理席位设置信息
        /// </summary>
        BCWP,
        /// <summary>
        /// 修正海压更新信息
        /// </summary>
        BQNH,
        /// <summary>
        /// 限制性空域状态信息
        /// </summary>
        BRTA,
        /// <summary>
        /// 机场跑道状态信息
        /// </summary>
        BRWY,
        /// <summary>
        /// 席位扇区分配信息
        /// </summary>
        BSEC,
        /// <summary>
        /// 二次代码分配、回收信息
        /// </summary>
        BSSR,
        /// <summary>
        /// 飞行计划协调数据
        /// </summary>
        CFPL,
        /// <summary>
        /// 飞行计划移交响应数据
        /// </summary>
        CHRP,
        /// <summary>
        /// 飞行计划移交请求数据
        /// </summary>
        CHRQ,
        /// <summary>
        /// 飞行计划移交逻辑确认数据
        /// </summary>
        CLAM
    }
}
