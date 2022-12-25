// -----------------------------------------------------------------------------
//  ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ATCer.Base;
using ATCer.LTFATCenter.Enums;

namespace ATCer.LTFATCenter.Dtos
{
    /// <summary>
    /// 飞行计划Dto
    /// </summary>
    public class FlightPlanDto: ATCerBaseDto<long>
    {
        [MaxLength(32)]
        public string GUFI { get; set; }
        /// <summary>
        /// 航班呼号
        /// </summary>
        [DisplayName("呼号")]
        [MaxLength(8)]
        public string Callsign { get; set; }
        /// <summary>
        /// 应答机编码
        /// </summary>
        [DisplayName("应答机")]
        [MaxLength(5)]
        public string SsrCode { get; set; }
        /// <summary>
        /// 飞行规则
        /// </summary>
        [DisplayName("飞行规则")]
        [MaxLength(1)]
        public string FlightRules { get; set; }
        [MaxLength(3)]
        public string FlightType { get; set; }
        public int AircraftNumber { get; set; }
        /// <summary>
        /// 航班型号
        /// </summary>
        [DisplayName("型号")]
        [MaxLength(4)]
        public string AircraftType { get; set; }
        /// <summary>
        /// 尾流
        /// </summary>
        [DisplayName("尾流")]
        public WakeTurbulence WTC { get; set; }
        [MaxLength(50)]
        public string AidEquipment { get; set; }
        [MaxLength(50)]
        public string SurEquipment { get; set; }
        [MaxLength(4)]
        public string ADEP { get; set; }
        [MaxLength(4)]
        public string ADES { get; set; }
        [MaxLength(4)]
        public string ADAR { get; set; }
        [MaxLength(4)]
        public string ALTN1 { get; set; }
        [MaxLength(4)]
        public string ALTN2 { get; set; }
        [MaxLength(4)]
        public string TotalEET { get; set; }
        [MaxLength(800)]
        public string FiledRoute { get; set; }
        [MaxLength(1000)]
        public string OtherInfo { get; set; }
        /// <summary>
        /// 计划撤轮挡时间
        /// </summary>
        public DateTimeOffset EOBT { get; set; }
        public DateTimeOffset? ETOT { get; set; }
        public DateTimeOffset? ELDT { get; set; }
        public DateTimeOffset? EIBT { get; set; }
        public DateTimeOffset? AOBT { get; set; }
        /// <summary>
        /// 实际起飞时间
        /// </summary>
        [DisplayName("起飞时间")]
        public DateTimeOffset? ATOT { get; set; }
        /// <summary>
        /// 实际落地时间
        /// </summary>
        [DisplayName("落地时间")]
        public DateTimeOffset? ALDT { get; set; }
        public DateTimeOffset? AIBT { get; set; }
        public DateTimeOffset? SOBT { get; set; }
        public DateTimeOffset? SIBT { get; set; }
        public DateTimeOffset? IOBT { get; set; }
        [MaxLength(1000)]
        public string ApprovedRoute { get; set; }
        [MaxLength(200)]
        public string AirwayCodes { get; set; }
        /// <summary>
        /// 注册号
        /// </summary>
        [DisplayName("注册号")]
        [MaxLength(20)]
        public string RegMarking { get; set; }
        [MaxLength(8)]
        public string ApprovedArcType { get; set; }
        [MaxLength(3)]
        public string Jurisdiction { get; set; }
        /// <summary>
        /// 航班先前
        /// </summary>
        [DisplayName("前状态")]
        public PlanStatus PrePlanState { get; set; }
        /// <summary>
        /// 计划状态
        /// </summary>
        [DisplayName("状态")]
        public PlanStatus PlanState { get; set; }
        [MaxLength(1000)]
        public string Remarks { get; set; }
        public bool FiledFlag { get; set; }
        public bool AbolishFlag { get; set; }
        public bool DeleteFlag { get; set; }
        [MaxLength(20)]
        public string ProcessUser { get; set; }
        [MaxLength(3)]
        public string CreateMode { get; set; }
        [MaxLength(20)]
        public string ReferSourceID { get; set; }
        [MaxLength(200)]
        public string ControlArea { get; set; }
        [MaxLength(3)]
        public string UpdateMode { get; set; }
    }
}
