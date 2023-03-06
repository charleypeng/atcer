// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using ATCer.DataCenter.Enums;
using ATCer.ElasticSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.DataCenter.Models.MH4016_9
{
    public class TestWalala:ATCElasticEntity<string>
    {
        public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.Now;
        public string Type { get; set; }
        public string Position { get; set; }

        public List<Testhey>? MyProperty { get; set; }
    }

    public class Testhey
    {
        public string Name { get; set; }
        public MetDataStatus Status { get; set; }
        public int? Value { get; set; }
    }

    public class Testhey2
    {
        public string Name { get; set; }
        public MetDataStatus Status { get; set; }
        public string Value { get; set; }
    }
}
