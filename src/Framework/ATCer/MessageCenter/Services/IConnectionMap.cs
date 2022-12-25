﻿// -----------------------------------------------------------------------------
//  ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.MessageCenter.Services
{
    public interface IConnectionMap<T>
    {
        public void Add(T key, string connectionId);
        public int Count { get;}
        public IEnumerable<string> GetConnections(T key);
        public void Remove(T key, string connectionId);
    }
}
