// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

global using DotNetCore.CAP;
global using System;
global using Microsoft.AspNetCore.SignalR;
global using System.Threading.Tasks;
global using Microsoft.Extensions.Logging;
global using Furion;
global using Topic = ATCer.Common.MQTopics;
global using Group = ATCer.Common.MQGroups;
global using Methods = ATCer.Common.MQMethods;
global using DotNetCore.CAP.Serialization;
global using ATCer.MessageQueue.Options;
global using Mapster;
global using Furion.ConfigurableOptions;
global using Microsoft.Extensions.Options;