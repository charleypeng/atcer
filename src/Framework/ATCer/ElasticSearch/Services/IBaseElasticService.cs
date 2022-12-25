// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight 2021  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.ElasticSearch.Services
{
    public interface IBaseElasticService<TIndex, TKey>
    {
        Task<TIndex> GetAsync(string id);
        Task DeleteAsync(TIndex product);
        Task SaveAsync(TIndex product);
        Task SaveManyAsync(IEnumerable<TIndex> products);
        Task SaveBulkAsync(IEnumerable<TIndex> products);
        Task<long> IndexCountAsync();
    }
}
