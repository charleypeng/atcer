// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2023  版权所有
// -----------------------------------------------------------------------------

using ATCer.ElasticSearch.Interfaces;
using Microsoft.Extensions.Configuration;
using Nest;

namespace ATCer.ElasticSearch.Core;

public class IndexCreator<T,TKey>:IESIndex, IESModelBuilder,IScoped where T:BaseElasticEntity<TKey>,new()
{
    private readonly  ILogger<IndexCreator<T, TKey>> _logger;
    private readonly IConfiguration _configuration;
    private ConnectionSettings settings;
    private IElasticClient _client;
    
    /// <summary>
    /// 
    /// </summary>
    public IndexCreator(ILogger<IndexCreator<T, TKey>> logger,
        IConfiguration configuration,
        string indexName = null!)
    {
        _logger = logger;
        _configuration = configuration;

        IndexName = string.IsNullOrWhiteSpace(indexName) ? typeof(T).Name : indexName;
        
        var url = _configuration["elasticsearch:url"];
        var userName = _configuration["elasticsearch:username"];
        var passWord = _configuration["elasticsearch:password"];

        if (url == null)
            throw new Exception("请告知知数据库url");
        
        settings = new ConnectionSettings(new Uri(url))
            .ServerCertificateValidationCallback((obj, cert, chain, sslPolicyErrors) => true)
            .BasicAuthentication(userName, passWord)
            .DefaultIndex(indexName);

        _client = new ElasticClient(settings);
    }
    
    /// <summary>
    /// 
    /// </summary>
    public virtual void InitIndex()
    {
        AddDefaultMappings(settings);
        CreateIndex(_client, IndexName);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="esSettings"></param>
    protected virtual void AddDefaultMappings(ConnectionSettings? esSettings)
    {
        settings?
            .DefaultMappingFor<T>(m => m
                .IdProperty("Id")
            );
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="client"></param>
    /// <param name="indexName"></param>
    protected virtual void CreateIndex(IElasticClient client, string indexName)
    {
        var createIndexResponse = client.Indices.Create(indexName,
            index => index.Map<T>(x => x.AutoMap())
        );
        _logger.LogInformation(createIndexResponse.Acknowledged
            ? $"ElasticSearch index:{indexName} created"
            : $"ElasticSearch index:{indexName} failed");
    }
    public string IndexName { get; set; }
    public Func<bool>? Mapping { get; set; }
}