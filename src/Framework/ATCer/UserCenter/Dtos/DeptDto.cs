// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Base;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ATCer.UserCenter.Dtos
{
    /// <summary>
    /// 部门信息
    /// </summary>
    [Description("部门信息")]
    public class DeptDto: BaseDto<int>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        [Required(ErrorMessage = "不能为空"), MaxLength(30, ErrorMessage = "最大长度不能大于{1}")]
        public string Name { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [DisplayName("联系人")]
        [MaxLength(20, ErrorMessage = "最大长度不能大于{1}")]
        public string Contacts { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [DisplayName("电话")]
        [MaxLength(20, ErrorMessage = "最大长度不能大于{1}")]
        public string Tel { get; set; }

        /// <summary>
        /// 资源排序
        /// </summary>
        [Required(ErrorMessage = "不能为空"), DefaultValue(0)]
        [DisplayName("排序")]
        public int Order { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [MaxLength(100, ErrorMessage = "最大长度不能大于{1}")]
        public string Remark { get; set; }

        /// <summary>
        /// 父级id
        /// </summary>
        [DisplayName("父级编号")]
        public int? ParentId { get; set; }

        /// <summary>
        /// 子集
        /// </summary>
        public ICollection<DeptDto> Children { get; set; }
    }
}
