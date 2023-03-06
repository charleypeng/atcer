// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Application.DataCenter.Extensions;
using ATCer.DataCenter.Enums;

namespace ATCer.DataCenter.Domains
{
    /// <summary>
    /// 
    /// </summary>
    public class MetData:ATCElasticEntity<string>
    {
        /// <summary>
        /// 
        /// </summary>
        public string? TYPE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? LOC { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Int32TypeData>? Int32TypeDatas { get; set; } = new List<Int32TypeData>();
        /// <summary>
        /// 
        /// </summary>
        public List<FloatTypeData>? FloatTypeDatas { get; set; } = new List<FloatTypeData>();
        /// <summary>
        /// 
        /// </summary>
        public List<StringTypeData>? StringTypeDatas { get; set; } = new List<StringTypeData>();
        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset? ReceiveTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>

        public static implicit operator MetData(RawMetData v)
        {
            return v.ConvertToMetData();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Int32TypeData:BaseMetData<int>
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public class FloatTypeData:BaseMetData<float>
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public class StringTypeData:BaseMetData<string>
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseMetData<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public string? DataTypeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public MetDataStatus Status { get; set; } = MetDataStatus.Normal;
        /// <summary>
        /// 
        /// </summary>
        public T? Value { get; set; }
    }
}
