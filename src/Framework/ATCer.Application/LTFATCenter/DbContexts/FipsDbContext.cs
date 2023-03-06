// -----------------------------------------------------------------------------
//  ATMS
//  gitee:https://gitee.com/charleypeng/ATCer 
//  Author and copyright Penglei all rights reserved
// -----------------------------------------------------------------------------

using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Furion;
using Furion.DatabaseAccessor;

namespace ATCer.LTFATCenter.DbContexts
{
    /// <summary>
    /// FIPS前置数据库
    /// 只用于获取SQL Server的数据
    /// </summary>
    [AppDbContext("FipsSqlServerConnectionString")]
    public class FipsDbContext: AppDbContext<FipsDbContext,FipsDbContextLocator>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public FipsDbContext(DbContextOptions<FipsDbContext> options) : base(options)
        {

        }
    }
}
