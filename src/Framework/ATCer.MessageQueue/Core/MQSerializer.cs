// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCore.CAP.Messages;
using DotNetCore.CAP.Serialization;
using System.Text.Json;

namespace ATCer.MessageQueue
{
    //public class MQSerializer : ISerializer
    //{
    //    public Message Deserialize(string json)
    //    {
    //        if (string.IsNullOrEmpty(json))
    //            return null;

    //        var message = JsonSerializer.Deserialize<Message>(json);
    //        return message;
    //    }

    //    public object Deserialize(object value, Type valueType)
    //    {
    //        if (value == null)
    //            return value;
    //        if (valueType == null)
    //            throw new ArgumentNullException(nameof(valueType));
    //        var strObj = JsonSerializer.Serialize(value);
    //        var obj = JsonSerializer.Deserialize<Type>(value);
    //    }

    //    public Task<Message> DeserializeAsync(TransportMessage transportMessage, Type valueType)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public bool IsJsonType(object jsonObject)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public string Serialize(Message message)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<TransportMessage> SerializeAsync(Message message)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
