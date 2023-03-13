// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using ATCer.DataCenter.Domains;
using ATCer.DataCenter.Enums;

namespace ATCer.Application.DataCenter.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class RawMetDataConverterExtension
    {
        public static MetData ConvertToMetData(this RawMetData rawMetData) 
        {
            return Convert(rawMetData);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rawMetData"></param>
        /// <returns></returns>
        public static MetData Convert(RawMetData rawMetData)
        {
            if (rawMetData == null)
                return null!;

            try
            {
                var metData = new MetData();
                
                metData.TYPE = rawMetData.TYPE;
                metData.LOC = rawMetData.LOC;
                if(rawMetData.TIME == null)
                {
                    metData.ReceiveTime = DateTimeOffset.Now;
                }
                else
                {
                    metData.ReceiveTime = DateTimeOffset.FromUnixTimeSeconds(rawMetData.TIME.Value);
                }
                
                if(rawMetData.DATA != null)
                {
                    var dict = new MetDataStatusDict();

                    foreach (var rdatas in rawMetData.DATA)
                    {
                        if(rdatas != null && rdatas.Count == 4)
                        {
                            switch(rdatas[1])
                            {
                                case MetDataTypeString.DString:
                                    metData.StringTypeDatas?.Add(new StringTypeData { DataTypeName = rdatas[0], Status = dict.Dict[rdatas[2]], Value = rdatas[3] });
                                    break;
                                case MetDataTypeString.DInteger:
                                    metData.FloatTypeDatas?.Add(new FloatTypeData() { DataTypeName = rdatas[0], Status = dict.Dict[rdatas[2]], Value = float.Parse(rdatas[3]) });
                                    break;
                                case MetDataTypeString.DFloat:
                                    metData.FloatTypeDatas?.Add(new FloatTypeData { DataTypeName = rdatas[0], Status = dict.Dict[rdatas[2]], Value =float.Parse(rdatas[3]) });
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                return metData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex); 
            }
        }
    }
}
