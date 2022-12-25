// -----------------------------------------------------------------------------
//  ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.Base.Domains
{
    /// <summary>
    /// Cache keys
    /// </summary>
    public class CacheKey : IEntity
    {
        /// <summary>
        /// Cache Key
        /// </summary>
        [Key]
        [Comment("CacheKey")]
        public string Key { get; set; }
    }
}
