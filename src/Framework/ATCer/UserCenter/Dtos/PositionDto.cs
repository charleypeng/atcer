// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ATCer.UserCenter.Dtos
{
    /// <summary>
    /// 岗位
    /// </summary>
    public class PositionDto : BaseDto<int>
    {
        /// <summary>
        /// 岗位名称
        /// </summary>
        [DisplayName("名称")]
        [Required(ErrorMessage = "不能为空")]
        public string Name { get; set; }

        /// <summary>
        /// 设置该岗位的目标
        /// </summary>
        [DisplayName("目标")]
        public string Target { get; set; }

        /// <summary>
        /// 职责
        /// </summary>
        [DisplayName("职责")]
        public string Duty { get; set; }

        /// <summary>
        /// 权利
        /// </summary>
        [DisplayName("权利")]
        public string Right { get; set; }

        /// <summary>
        /// 岗位等级
        /// </summary>
        [DisplayName("岗位等级")]
        public string Grade { get; set; }

        /// <summary>
        /// 岗位薪资
        /// </summary>
        [DisplayName("岗位薪资")]
        public string Salary { get; set; }


        /// <summary>
        /// 任职资格
        /// </summary>
        [DisplayName("任职资格")]
        public string Qualifications { get; set; }
    }
}
