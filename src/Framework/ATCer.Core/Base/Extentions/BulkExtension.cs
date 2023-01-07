// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.Core.Base.Extentions
{
    public static class BulkExtension
    {
        public static async Task MyBulkInsertOrUpdate<T>(this IQueryable<T> query, CancellationToken cancellationToken = default)
        {
            await query.ExecuteDeleteAsync(cancellationToken);
        }
    }
}
