using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ATCer.LTFATCenter.Enums
{
    /// <summary>
    /// 计划状态
    /// </summary>
    public enum PlanStatus
    {
        /// <summary>
        /// 为空
        /// </summary>
        [Description("空")]
        NULL,
        /// <summary>
        /// ？？
        /// </summary>
        [Description("FUT")]
        FUT,
        /// <summary>
        /// 未激活
        /// </summary>
        [Description("未激活")]
        IAC,
        /// <summary>
        /// 计划审批
        /// </summary>
        [Description("审批")]
        PLN,
        /// <summary>
        /// 计划审批通过
        /// </summary>
        [Description("已审批")]
        FPL,
        /// <summary>
        /// 已发起飞报      
        /// </summary>
        [Description("已起飞")]
        DEP,
        /// <summary>
        /// 已发落地报
        /// </summary>
        [Description("落地")]
        ARR,
        /// <summary>
        /// 在相邻管制
        /// </summary>
        [Description("相邻")]
        SUR,
        /// <summary>
        /// 计划结束
        /// </summary>
        [Description("结束")]
        FIN,
        /// <summary>
        /// 状态未知
        /// </summary>
        [Description("未知")]
        UNKNOWN
    }

    /// <summary>
    /// 
    /// </summary>
    public static class PlanStatusExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static PlanStatus ToPlanStatus(this string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                return PlanStatus.UNKNOWN;
            try
            {
                PlanStatus p = PlanStatus.UNKNOWN;
                var s = Enum.TryParse(status.ToUpper(), out p);
                if (s)
                {
                    return p;
                }
                else
                {
                    return PlanStatus.UNKNOWN;
                }
            }
            catch
            {
                return PlanStatus.UNKNOWN;
            }
        }
    }
}
