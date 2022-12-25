// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.NotificationSystem.Dtos;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ATCer.NotificationSystem.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class NotificationDataJsonConverter : JsonConverter<NotificationData>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override NotificationData Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Utf8JsonReader readerCopy = reader;
            string typeAssemblyName = null;
            while (readerCopy.Read())
            {
                if (readerCopy.TokenType == JsonTokenType.PropertyName)
                {
                    if (readerCopy.GetString().ToLower().Equals("typeassemblyname"))
                    {
                        readerCopy.Read();
                        if (readerCopy.TokenType == JsonTokenType.String)
                        {
                            typeAssemblyName = readerCopy.GetString();
                        }
                    }
                }
            }
            if (string.IsNullOrEmpty(typeAssemblyName))
            {
                throw new JsonException($"{nameof(NotificationData.TypeAssemblyName)} is required");
            }
            Type t = Type.GetType(typeAssemblyName);
            if (t == null)
            {
                throw new JsonException($"{typeAssemblyName} type is not find");
            }
            var newOptions =new JsonSerializerOptions(options);
            newOptions.PropertyNameCaseInsensitive = true;
            newOptions.IncludeFields = true;
            object data= JsonSerializer.Deserialize(ref reader, t, newOptions);
            return (NotificationData)data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, NotificationData value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();

            JsonSerializer.Serialize(writer, value, options);

            writer.WriteEndArray();
        }
    }
}
