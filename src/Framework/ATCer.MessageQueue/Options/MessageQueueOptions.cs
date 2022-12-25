// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Furion;

namespace ATCer.MessageQueue
{
    /// <summary>
    /// 
    /// </summary>
    public class MessageQueueOptions: IConfigurableOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public const string Default = "MessageQueue";
        /// <summary>
        /// Group Id
        /// </summary>
        public string GroupId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DbProvider { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MQProvider { get; set; }
        /// <summary>
        /// 跨域
        /// </summary>
        public string[] Origins { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public RabitMQOptions RabitMQ { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public KalfkaOptions Kalfka { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public PostreSqlOptions PostgreSql { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public SqlServerOptions SqlServerOptions { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public MySqlOptions MySql { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public CAPNodeOptions CAPNode { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SqlServerOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public string ConnectionString { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class PostreSqlOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public string ConnectionString { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class MySqlOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public string ConnectionString { get; set; }
    }
}
