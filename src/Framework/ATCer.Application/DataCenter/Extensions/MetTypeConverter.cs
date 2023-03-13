// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2023  版权所有
// -----------------------------------------------------------------------------

using ATCer.DataCenter.Domains;
using System.Reflection;
using ATCer.Application.DataCenter.Domains.MetData;
using ATCer.Common;
using ATCer.DataCenter;
using ATCer.DataCenter.Enums;

namespace ATCer.DataCenter;

/// <summary>
/// TO convert type into correct met data type
/// </summary>
public static class MetTypeConverterCore
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="rawData"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static T? MetTypeConverter<T>(RawMetData rawData) where T : class ,new()
    {
        var metDict = new MetDataStatusDict();
        if (rawData == null)
            return default(T);
        if (rawData.TYPE?.ToUpper() != typeof(T).Name)
        {
            throw new Exception("the given met raw data type is not the expected type");
        }
        else
        {
            if (rawData.DATA == null)
                return null;

            var typeFromHandle = typeof(T);
            var obj = Activator.CreateInstance(typeFromHandle);

            var properties = typeFromHandle.GetProperties();
            //set the time from unix time
            var timeProperty = properties.FirstOrDefault(x => x.Name == "CreatedTime");
            //set location
            var locProperty = properties.FirstOrDefault(x => x.Name == "Location");
            locProperty?.SetValue(obj, rawData.LOC);
            if (timeProperty != null)
            {
                try
                {
                    if (rawData.TIME != null)
                    {
                        timeProperty.SetValue(obj, DateTimeOffset.FromUnixTimeSeconds(rawData.TIME.Value));
                    }
                    else
                    {
                        timeProperty.SetValue(obj, DateTimeOffset.FromUnixTimeSeconds(DateTimeOffset.Now.ToUnixTimeSeconds()));
                    }
                }
                catch (Exception e)
                {
                    timeProperty.SetValue(obj, DateTimeOffset.FromUnixTimeSeconds(DateTimeOffset.Now.ToUnixTimeSeconds()));
                }
            }
            //set properties value
            foreach (var data in rawData.DATA)
            {
                if (data?.Count != 4)
                    throw new Exception("not valid met raw data");
                
                var property = properties.FirstOrDefault(x => x.Name == data?[0]?.ToUpper());
                if (property != null)
                {
                    switch (data?[1])
                    {
                        case MetDataTypeString.DFloat:
                            if (string.IsNullOrWhiteSpace(data[3]))
                            {
                                var strData = new MetTuple
                                {
                                    Status = metDict.Dict[data[2]!],
                                    Value = null
                                };
                                property.SetValue(obj, strData);
                            }
                            else
                            {
                                var strData = new MetTuple
                                {
                                    Status = metDict.Dict[data[2]!],
                                    Value = float.Parse(data[3]!) 
                                };
                                property.SetValue(obj, strData);
                            }
                            break;
                        case MetDataTypeString.DInteger:
                            if (string.IsNullOrWhiteSpace(data[3]))
                            {
                                var strData = new MetTuple
                                {
                                    Status = metDict.Dict[data[2]!],
                                    Value = null
                                };
                                property.SetValue(obj, strData);
                            }
                            else
                            {
                                var strData = new MetTuple
                                {
                                    Status = metDict.Dict[data[2]!],
                                    Value = float.Parse(data[3]!) 
                                };
                                property.SetValue(obj, strData);
                            }
                            break;
                        
                        default:
                            if (string.IsNullOrWhiteSpace(data[3]))
                            {
                                var strData = new MetTuple
                                {
                                    Status = metDict.Dict[data[2]],
                                    Value = null
                                };
                                property.SetValue(obj, strData);
                            }
                            else
                            {
                                var strData = new MetTuple<string>
                                {
                                    Status = metDict.Dict[data[2]],
                                    Value = data[3]
                                };
                                property.SetValue(obj, strData);
                            }
                            break;
                        }
                }
            }
            return obj as T;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T? ToMetType<T>(this RawMetData data) where T:class,new()
    {
        return MetTypeConverter<T>(data);
    }
}