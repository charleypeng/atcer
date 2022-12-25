/* This project is part of the CBTSoft, which is a private license under the legal of laws.
 * Any copy or use may cause legal problem.
 * Author: Peng Lei
 * Created Date:2019-1-20 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ATCer.LTFATCenter.Enums
{
    /// <summary>
    /// 飞行状态
    /// </summary>
    [Description("状态")]
    public enum FlightStatus
    {
        /// <summary>
        /// 空
        /// </summary>
        [Description("空")]
        NULL,
        /// <summary>
        /// 计划
        /// </summary>
        [Description("计划")]
        PLN,
        /// <summary>
        /// 准备
        /// </summary>
        [Description("准备")]
        PRE,
        /// <summary>
        /// 申请放行
        /// </summary>
        [Description("申请放行")]
        REQ,
        /// <summary>
        /// 已放行
        /// </summary>
        [Description("已放行")]
        CLD,
        /// <summary>
        /// 准备好
        /// </summary>
        [Description("准备好")]
        RDY,
        /// <summary>
        /// 未推出
        /// </summary>
        [Description("未推出")]
        NPU,
        /// <summary>
        /// 开车
        /// </summary>
        [Description("开车")]
        STR,
        /// <summary>
        /// 推出
        /// </summary>
        [Description("推出")]
        PUS,
        /// <summary>
        /// 推出开车
        /// </summary>
        [Description("推开")]
        PS,
        /// <summary>
        /// 推出_开车
        /// </summary>
        [Description("??")]
        P_S,
        /// <summary>
        /// 滑出
        /// </summary>
        [Description("滑出")]
        TAX,
        /// <summary>
        /// 排队
        /// </summary>
        [Description("排队")]
        QUE,
        /// <summary>
        /// 进跑道
        /// </summary>
        [Description("进跑道")]
        LIN,
        /// <summary>
        /// 起飞
        /// </summary>
        [Description("起飞")]
        TAK,
        /// <summary>
        /// 终止起飞
        /// </summary>
        [Description("终止")]
        ABT,
        /// <summary>
        /// 结束
        /// </summary>
        [Description("结束")]
        FIN,
        /// <summary>
        /// 穿越跑道
        /// </summary>
        [Description("穿越")]
        CRS,
        /// <summary>
        /// 跑到外等待
        /// </summary>
        [Description("等待")]
        CRH,
        /// <summary>
        /// 脱离滑行
        /// </summary>
        [Description("滑行")]
        CRT,
        /// <summary>
        /// 脱离
        /// </summary>
        [Description("脱离")]
        VAC,
        /// <summary>
        /// 落地
        /// </summary>
        [Description("落地")]
        LND,
        /// <summary>
        /// 继续进近
        /// </summary>
        [Description("继续进近")]
        CNT,
        /// <summary>
        /// 进近
        /// </summary>
        [Description("进近")]
        APP,
        /// <summary>
        /// 到达
        /// </summary>
        [Description("到达")]
        ARR,
        /// <summary>
        /// 滑出
        /// </summary>
        [Description("滑出")]
        Unknown,
    }

    /// <summary>
    /// 飞行状态扩展
    /// </summary>
    public static class FlightStatusExtension
    {
        /// <summary>
        /// 转换为飞行状态
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static FlightStatus ToFlightStatus(this string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                return FlightStatus.NULL;
            // to make sure not p_s status
            status = status.ToUpper(); 
            try
            {
                FlightStatus o = FlightStatus.Unknown;
                var s = Enum.TryParse(status.ToUpper(), out o);
                if (s)
                {
                    return o;
                }
                else
                {
                    return FlightStatus.Unknown;
                }
            }
            catch
            {
                return FlightStatus.Unknown;
            }
        }
    }
}
