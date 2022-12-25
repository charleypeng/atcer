/* This project is part of the CBTSoft, which is a private license under the legal of laws.
 * Any copy or use may cause legal problem.
 * Author: Peng Lei
 * Created Date:2019-1-20 
 */
using System.ComponentModel;

namespace ATCer.LTFATCenter.Enums
{
    /// <summary>
    /// 计划类型
    /// </summary>
    public enum PlanType
    {
        /// <summary>
        /// 离港航班
        /// </summary>
        [Description("离港")]
        Departure,
        /// <summary>
        /// 进港航班
        /// </summary>
        [Description("进港")]
        Arrival,
        /// <summary>
        /// 训练飞行
        /// </summary>
        [Description("训练")]
        Training,
        /// <summary>
        /// 拖拽航班
        /// </summary>
        [Description("拖拽")]
        Drag,
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        Unknown
    }

    /// <summary>
    /// 
    /// </summary>
    public static class FlightTypesExtention
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public static Dictionary<PlanType, string> ToFlightTypeDict(this PlanType types)
        {
            var dict = new Dictionary<PlanType,string>();
            switch(types)
            {
                case PlanType.Arrival:
                    dict.Add(types, "STAR");
                    break;
                case PlanType.Departure:
                    dict.Add(types, "SID");
                    break;
                default:
                    dict.Add(types, types.ToString());
                    break;
            }
            return dict;
        }
    }
}
