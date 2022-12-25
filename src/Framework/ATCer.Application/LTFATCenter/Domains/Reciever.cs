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
using ATCer.ElasticSearch;

namespace ATCer.LTFATCenter.Domains
{
    /// <summary>
    /// 接受数据模型
    /// </summary>
    public class Reciever<T>:BaseElasticEntity<string> where T:class
    {
        ///<summary>
        ///Indicate a data type
        ///</summary>
        public string type { get; set; }
        public IEnumerable<T> data { get; set; }
        public string msg { get; set; }
        public DateTime recordtime { get; set; }
        public Reciever()
        {
            recordtime = DateTime.Now;
        }
    }
}
