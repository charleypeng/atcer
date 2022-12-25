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
global using DotNetCore.CAP;
global using ATCer;
global using ATCer.Base;
global using ATCer.LTFATCenter.Domains;
global using ATCer.LTFATCenter.Enums;
global using ATCer.LTFATCenter.DbContexts;
global using Topic = ATCer.Common.MQTopics;
global using Group = ATCer.Common.MQGroups;