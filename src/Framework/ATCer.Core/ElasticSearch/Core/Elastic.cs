// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight 2021  版权所有 
// -----------------------------------------------------------------------------

using Nest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Furion;

namespace ATCer.ElasticSearch.Core
{
    /// <summary>
    /// Elastic base class
    /// </summary>
    public class Elastic<TEntity>: ElasticClient where TEntity:class
    {
        private string configString;
        private void getAttributes()
        {
            var t = typeof(TEntity);
            var connectionAttr = t.GetCustomAttributes(typeof(SetConfig), false).FirstOrDefault();

            if(connectionAttr != null)
                configString = ((SetConfig)connectionAttr).ConfigString;
        }
        /// <summary>
        /// 
        /// </summary>
        public ConnectionSettings Settings { get; private set; }

        /// <summary>
        /// 带参数初始化
        /// </summary>
        /// <param name="settings"></param>
        public Elastic(ConnectionSettings settings):base(settings)
        {
            Settings = settings;
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void build()
        {
            var configuration = App.Configuration;
            var url = configuration["elasticsearch:url"];
            var defaultIndex = configuration["elasticsearch:index"];
            var userName = configuration["elasticsearch:username"];
            var passWord = configuration["elasticsearch:password"];

            Settings = new ConnectionSettings(new Uri(url))
                .BasicAuthentication(userName, passWord)
                .DefaultIndex(defaultIndex);
        }
        /// <summary>
        /// 已通过Attributes初始化
        /// </summary>
        public Elastic()
        {
            getAttributes();
            if (string.IsNullOrWhiteSpace(configString))
                throw new ArgumentNullException("no given attributes");
            build();

        }

        public Action<ConnectionSettings> AddDefaultMappings { get; set; }
        //private static void AddDefaultMappings(ConnectionSettings settings)
        //{
        //    settings
        //        .DefaultMappingFor<TEntity>(m => m
        //        //.Ignore(p => p.OtherInfo)
        //        );
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="indexName"></param>
        public virtual void CreateIndex(IElasticClient client, string indexName)
        {
            var createIndexResponse = client.Indices.Create(indexName,
                index => index.Map<TEntity>(x => x.AutoMap())
            );
        }
    }
}
