// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace ATCer.Client.Base
{
    public class TableSearchField
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public Type Type { get; set; }

        public string Value { get; set; }

        public IEnumerable<string> Values { get; set; }

        public bool Multiple { get; set; }
    }
}
