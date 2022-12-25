// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATCer.LTFATCenter.Enums;
using Furion.DatabaseAccessor;
using ATCer.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ATCer.LTFATCenter.Domains
{
    /// <summary>
    /// SID and STAR
    /// </summary>
    public class SADProcedure:IEntity<MasterDbContextLocator>
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        [Comment("Id")]
        public int Id { get; set; }
        /// <summary>
        /// 进离港程序
        /// </summary>
        [Comment("进离港程序")]
        public ProcedureMethod Method { get; set; }
        /// <summary>
        /// 程序名称
        /// </summary>
        [MaxLength(10)]
        [Comment("名称(DAP-9X)")]
        public string Name { get; set; }
        /// <summary>
        /// 方向
        /// </summary>
        [Comment("方向")]
        public SADPDirection Direction { get; set; }
        /// <summary>
        /// 前缀
        /// </summary>
        [MaxLength(4)]
        [Comment("前缀")]
        public string PrefixCode { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(50)]
        [Comment("备注")]
        public string Remarks { get; set; }
    }
}
