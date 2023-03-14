// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using Newtonsoft.Json;

namespace ATCer.DataCenter.Domains.MH4016_9
{
    /// <summary>
    /// 跑道灯光
    /// </summary>
    public class RWYLIGHTS
    {
        public MetTuple LIGHTS { get; set; }
        public MetTuple<bool?> ISMANUAL { get; set; }
        public MetTuple<string?> INFO { get; set; }

        [JsonProperty("18R_InUse")]
        public MetTuple RW18R_InUse { get; set; }

        [JsonProperty("36L_InUse")]
        public MetTuple RW36L_InUse { get; set; }
    }
}
