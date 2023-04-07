// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using Magicodes.ExporterAndImporter.Core;
using ATCer.HRCenter.Enums;

namespace ATCer.HRCenter.Dtos
{
    /// <summary>
    /// 导入执勤数据
    /// </summary>
    public class ImportTimeItemDto
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [ImporterHeader(Name = "姓名")]
        public string? ControllerName { get; set; }
        /// <summary>
        /// 扇区
        /// </summary>
        [ImporterHeader(Name = "扇区")]
        public string SectorCode { get; set; }
        /// <summary>
        /// 物理地址
        /// </summary>
        [ImporterHeader(Name = "物理地址")]
        public string PysicalPosition { get; set; }
        /// <summary>
        /// 席位类型
        /// </summary>
        [ImporterHeader(Name = "席位类型")]
        public string PositionType { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [ImporterHeader(Name = "开始时间")]
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [ImporterHeader(Name = "结束时间")]
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 值班类型
        /// </summary>
        [ImporterHeader(Name = "值班类型")]
        [Required(ErrorMessage = "岗位角色不能为空")]
        public ControllerRole? ControllerRole { get; set; }
    }
}