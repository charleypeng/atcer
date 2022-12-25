// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.HRCenter.Enums;
using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace ATCer.HRCenter.Domains
{
    /// <summary>
    /// 岗位类型
    /// </summary>
    [Comment("岗位类型")]
    public class ATCPositionRole : ATCer.Base.GardenerEntityBase
    {
        /// <summary>
        /// 代号
        /// </summary>
        [MaxLength(20)]
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(10)]
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(100)]
        public string Description { get; set; }
    }
}
