// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2023  版权所有
// -----------------------------------------------------------------------------

using ATCer.DataCenter.Domains;
using ATCer.DataCenter.Enums;
using System.Reflection;

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
    public static T? MetTypeConverter<T>(RawMetData rawData) where T : class, new()
    {
        var metDict = new MetDataStatusDict();

        if (rawData == null)
            return default(T);

        var typeName = typeof(T).Name.ToUpper();
        var rawDataTypeName = rawData.TYPE?.ToUpper();

        //to make sure class name and data type name are the same
        if (typeName.EndsWith("DTO"))
            rawDataTypeName = rawDataTypeName + "DTO";

        if (!typeName.Equals(rawDataTypeName))
            throw new Exception("the given met raw data type is not the expected type");

        if (rawData.DATA == null)
            return null;

        var typeFromHandle = typeof(T);
        var obj = Activator.CreateInstance(typeFromHandle);
        var properties = typeFromHandle.GetProperties();
        //set the time from unix time
        var timeProperty = properties.FirstOrDefault(x => x.Name == "CreatedTime");

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

        //set location
        var locProperty = properties.FirstOrDefault(x => x.Name == "Location");

        locProperty?.SetValue(obj, rawData.LOC);

        //set properties value
        try
        {
            //get property dict
            var dict = GetDataNames<T>();
            PropertyInfo? property;

            foreach (var data in rawData.DATA)
            {
                if (data == null || data?.Count != 4)
                    throw new Exception("not valid met raw data");

                //define use dataname as property or use property's own name
                var dataName = data?[0];
                var disPropName = string.Empty;

                if (dataName != null)
                {
                    if (dict.ContainsKey(dataName))
                    {
                        var dicPropName = dict[dataName];
                        if (string.IsNullOrWhiteSpace(dicPropName))
                        {
                            property = properties?.FirstOrDefault(x => x.Name == dataName);
                        }
                        else
                        {
                            property = properties?.FirstOrDefault(x => x.Name == dicPropName);
                        }
                    }
                    else
                    {
                        property = properties?.FirstOrDefault(x => x.Name == dataName);

                    }

                }
                else
                {
                    throw new Exception("the given data property name can not be null");
                }

                if (property != null)
                {
                    //fix PW integer error
                    //if (PWHighLights.Contains(data?[0]))
                    //{
                    //    data![1] = MetDataTypeString.DString;
                    //}
                    #region set property
                    switch (data?[1])
                    {
                        case MetDataTypeString.DFloat:
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
                            if (string.IsNullOrWhiteSpace(data?[3]))
                            {
                                var strData = new MetTuple<string>
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
                    #endregion
                }
            }
            return obj as T;
        }
        catch (Exception ex)
        {
            throw new Exception("无法倒装成气象类数据", ex);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T? ToMetType<T>(this RawMetData data) where T : class, new()
    {
        return MetTypeConverter<T>(data);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Dictionary<string, string> GetDataNames<T>()
    {
        Dictionary<string, string> _dict = new Dictionary<string, string>();

        PropertyInfo[] props = typeof(T).GetProperties();
        foreach (PropertyInfo prop in props)
        {
            object[] attrs = prop.GetCustomAttributes(true);
            foreach (object attr in attrs)
            {
                DataName? dataName = attr as DataName;
                if (dataName != null)
                {
                    string displayName = dataName.DisplayName;

                    _dict.Add(displayName, prop.Name);
                }
            }
        }
        return _dict;
    }

    public static string[] RunwayStrs => new string[] 
    {
        "18R_InUse","18L_InUse","36R_InUse","36L_InUse"
    };

    public static string[] PWHighLights => new string[]
    {
        "PW","RW","WXNWS"
    };
}