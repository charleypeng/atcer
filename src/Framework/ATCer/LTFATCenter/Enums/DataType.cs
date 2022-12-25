// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight 2021  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ATCer.LTFATCenter.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DataType
    {
        CAT062,
        CAT048,
        ERROR,
        UNKNOWN
    }
}
