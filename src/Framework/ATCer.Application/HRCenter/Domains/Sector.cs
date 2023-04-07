// -----------------------------------------------------------------------------
//  ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.HRCenter.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace ATCer.HRCenter.Domains
{
    /// <summary>
    /// 扇区表
    /// </summary>
    [Comment("扇区信息")]
    public class Sector : ATCerEntityBase, IBaseEntity
    {
        /// <summary>
        /// 扇区代码
        /// </summary>
        [DisplayName("扇区代码")]
        [MaxLength(10)]
        [Required]
        public string Code { get; set; }
        /// <summary>
        /// 扇区名称
        /// </summary>
        [DisplayName("扇区名称")]
        [MaxLength(10)]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 扇区系数
        /// </summary>
        [DisplayName("扇区系数")]
        [Required]
        public double Multiplier { get; set; }
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
        /// 席位名称
        /// </summary>
        [DisplayName("席位名称")]
        public string PositionName { get; set; }
        /// <summary>
        /// 席位类型
        /// </summary>
        [DisplayName("席位类型")]
        public PositionRole Position { get; set; }
        /// <summary>
        /// 重点席位
        /// </summary>
        [DisplayName("重点席位")]
        [Required]
        public bool Cat3Sector { get; set; } = false;
    }
}
