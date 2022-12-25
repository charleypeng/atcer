// -----------------------------------------------------------------------------
//  ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Base;
using ATCer.HRCenter.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ATCer.HRCenter.Dtos
{
    /// <summary>
    /// 扇区Dto
    /// </summary>
    public class SectorDto: ATCerBaseDto<int>
    {
        private string _code = string.Empty;
        /// <summary>
        /// 扇区代码
        /// </summary>
        [DisplayName("扇区代码")]
        [MaxLength(10)]
        [Required]
        public string Code { get { return _code; } set { _code = value.ToUpper().Trim(); } }
        /// <summary>
        /// 扇区名称
        /// </summary>
        [DisplayName("扇区名称")]
        [MaxLength(10)]
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 扇区系数
        /// </summary>
        [DisplayName("扇区系数")]
        [Required]
        public double Multiplier { get; set; } = 1;
        /// <summary>
        /// 管制部门
        /// </summary>
        [DisplayName("管制部门")]
        public ATCDepartment Department { get; set; }
        /// <summary>
        /// 物理席位
        /// </summary>
        [DisplayName("物理席位")]
        public string PhysicalPosition { get; set; }
        /// <summary>
        /// 席位类型
        /// </summary>
        [DisplayName("席位类型")]
        public PositionRole Position { get; set; }
        /// <summary>
        /// 席位名称
        /// </summary>
        [DisplayName("席位名称")]
        public string PositionName { get; set; }
    }
}
