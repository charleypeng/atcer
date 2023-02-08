// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------
global using System;
global using System.Linq;
global using System.Collections.Generic;
global using System.Threading.Tasks;
global using Furion.DatabaseAccessor;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Logging;
global using Mapster;
global using ATCer;
global using ATCer.Base;
global using ATCer.Enums;
global using Furion;
global using Furion.DependencyInjection;
global using Furion.FriendlyException;
global using Topic = ATCer.Common.MQTopics;
global using Group = ATCer.Common.MQGroups;