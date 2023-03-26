// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Timers;
using System.Net.Sockets;
using Caching;
using System.Net;

namespace ATCer.DataRecorder;

/// <summary>
/// for multicast
/// </summary>
public class MUdpRecorder : IRecorder
{
    #region Public-Members

    /// <summary>
    /// Event to fire when a new endpoint is detected.
    /// </summary>
    public event EventHandler<EndpointMetadata> EndpointDetected;

    /// <summary>
    /// Event to fire when a datagram is received.
    /// </summary>
    public event EventHandler<Datagram> DatagramReceived;

    /// <summary>
    /// Retrieve a list of (up to) the 100 most recently seen endpoints.
    /// </summary>
    public List<string> Endpoints
    {
        get
        {
            return _RemoteSockets.GetKeys();
        }
    }

    /// <summary>
    /// Maximum datagram size, must be greater than zero and less than or equal to 65507.
    /// </summary>
    public int MaxDatagramSize
    {
        get
        {
            return _MaxDatagramSize;
        }
        set
        {
            if (value < 1 || value > 65507) throw new ArgumentException("MaxDatagramSize must be greater than zero and less than or equal to 65507.");
            _MaxDatagramSize = value;
        }
    }

    /// <summary>
    /// Events.
    /// </summary>
    public SimpleUdpEvents Events
    {
        get
        {
            return _Events;
        }
        set
        {
            if (value == null) throw new ArgumentNullException(nameof(Events));
            _Events = value;
        }
    }

    #endregion
    //public properties
    public DataEncodings Encodings { get; }
    public bool IsConnected { get; private set; } = false;
    public event EventHandler<Datagram>? DataReceived;
    public string Ip { get; private set; }
    private IPAddress address;
    public int Port { get; private set; }
    public Datagram RecordData { get; private set; }
    public int RetryInterval { get; private set; } = 5;
    //private field
    UdpClient client;
    private readonly ILogger<MUdpRecorder> _logger;
    private readonly IOptions<DataRecorderOptions> _options;
    private readonly RecorderOptions recorderOptions = new RecorderOptions();
    private string name;
    private System.Timers.Timer retrier;
    private LRUCache<string, Socket> _RemoteSockets = new LRUCache<string, Socket>(100, 1, prepopulate: false);
    private int _MaxDatagramSize = 65507;
    private SimpleUdpEvents _Events;
    //work as a thread
    private Thread task;
    //thread work flag
    private bool flag = true;
    //job action 
    private Action<Datagram> _action;
    /// <summary>
    /// Init
    /// </summary>
    /// <param name="options"></param>
    /// <param name="logger"></param>
    /// <exception cref="Exception"></exception>
    public MUdpRecorder(IOptions<DataRecorderOptions> options,
                             ILogger<MUdpRecorder> logger)
    {
        _logger = logger;
        _options = options;
        //init encoding
        Encodings = recorderOptions.Encoding;
        //check options
        if (_options.Value == null)
            throw new ArgumentNullException(nameof(options));
        if (_options.Value.Recorders == null || _options.Value.Recorders.Count() == 0)
            throw new ArgumentNullException("please at least configure one recorder");

        var typeName = this.GetType().Name;
        recorderOptions = _options.Value.Recorders.FirstOrDefault(x => x.RecorderName == typeName);
        if (recorderOptions == null)
            throw new ArgumentNullException(nameof(RecorderOptions));

        _action = recorderOptions.JobAction;
        //init timer
        OnInitialize();
    }

    private void Retier_Elapsed(object? sender, ElapsedEventArgs e)
    {
        if (IsConnected)
            return;

        Start();
    }

    private void Client_EndpointDetected(object? sender, EndpointMetadata em)
    {
        _logger.LogInformation("Endpoint detected: " + em.Ip + ":" + em.Port);
    }

    private void Client_DatagramReceived(object? sender, Datagram e)
    {
        RecordData = e;
        RaiseData(sender, RecordData);
    }

    protected virtual void OnInitialize()
    {
        //init fields
        name = this.GetType().Name;
        Ip = recorderOptions.Ip;
        if(string.IsNullOrWhiteSpace(Ip))
            throw new ArgumentNullException($"IP {this.GetType().Name}");
        Port = recorderOptions.Port;
        var isIp = IPAddress.TryParse(Ip, out address);
        if (!isIp)
            throw new Exception($"the given ip {Ip} is not valid");
        //init udp client
        client = new UdpClient(Port);
        client.JoinMulticastGroup(address);
        this.DatagramReceived += Client_DatagramReceived;
        this.EndpointDetected += Client_EndpointDetected;

        RecordData = new Datagram(Ip, Port, new Byte[64]);
        retrier = new System.Timers.Timer(TimeSpan.FromSeconds(RetryInterval).TotalMilliseconds);
        retrier.Elapsed += Retier_Elapsed;
        retrier.Enabled = false;
    }

    public virtual void Start()
    {
        try
        {
            this.internalStart();
            IsConnected = true;
            _logger.LogInformation($"{name} started");
        }
        catch (Exception ex)
        {
            IsConnected = false;
            _logger.LogError($"{name} failed to connect to {Ip}:{Port}");
#if DEBUG
            _logger.LogError(ex.ToString());
#endif
        }
    }

    public virtual void StartWithRetry(int interval)
    {
        RetryInterval = interval;
        retrier.Interval = TimeSpan.FromSeconds(RetryInterval).TotalMilliseconds;
        retrier.Enabled = true;
        retrier.Start();
    }

    public virtual void Stop()
    {
        this.internalStop();
        retrier.Stop();
        IsConnected = false;
        _logger.LogInformation($"{name} stopped");
    }

    private void RaiseData(object? obj, Datagram e)
    {
        DataReceived?.Invoke(obj, e);
    }

    public virtual void Dispose()
    {
        this.Stop();
        client.Dispose();
        retrier.Dispose();
        GC.SuppressFinalize(this);
    }


    private void internalStart(CancellationToken cancellationToken = default)
    {
        task = new Thread(async () =>
        {
            while (flag)
            {
                try
                {
                    if (client.Available <= 0) continue;
                    if (client.Client == null) return;

                    var data = await client.ReceiveAsync(cancellationToken);

                    if (_action != null)
                    {
                        RecordData = new Datagram(Ip, Port, data.Buffer);
                        _action?.Invoke(RecordData);
                    }
                }
                catch (Exception ex)
                {
                    flag = false;
                    IsConnected = false;
                }
            }
        });
    }

    private void internalStop()
    {
        flag = false;
        if(task.ThreadState == ThreadState.Running)
        {
            task.Abort();
        }
        client.Close();
    }
    public async void SendData(object data)
    {
        var str = JsonSerializer.Serialize(value: data);
        //await client.SendAsync(Ip, Port, Encoding.UTF8.GetBytes(str));
    }
}
