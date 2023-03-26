// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Timers;
using System.Text.Json;

namespace ATCer.DataRecorder
{
    /// <summary>
    /// A simple udp data recorder
    /// </summary>
    public class SimpleUdpRecorder : IRecorder
    {
        //public properties
        public DataEncodings Encodings { get; }
        public bool IsConnected { get; private set; } = false;
        public event EventHandler<Datagram>? DataReceived;
        public string Ip { get; private set; }
        public int Port { get; private set; }
        public Datagram RecordData { get; private set; }
        public int RetryInterval { get; private set; } = 5;
        //private field
        UdpEndpoint client;
        private readonly ILogger<SimpleUdpRecorder> _logger;
        private readonly IOptions<DataRecorderOptions> _options;
        private readonly RecorderOptions recorderOptions = new RecorderOptions();
        private string name;
        private System.Timers.Timer retrier;

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="options"></param>
        /// <param name="logger"></param>
        /// <exception cref="Exception"></exception>
        public SimpleUdpRecorder(IOptions<DataRecorderOptions> options,
                                 ILogger<SimpleUdpRecorder> logger)
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
            Port = recorderOptions.Port;
            //init udp client
            client = new UdpEndpoint(Port, Ip, recorderOptions.EndpointType);
            client.DatagramReceived += Client_DatagramReceived;
            client.EndpointDetected += Client_EndpointDetected;

            RecordData = new Datagram(Ip, Port, new Byte[64]);
            retrier = new System.Timers.Timer(TimeSpan.FromSeconds(RetryInterval).TotalMilliseconds);
            retrier.Elapsed += Retier_Elapsed;
            retrier.Enabled = false;
        }

        public virtual void Start()
        {
            try
            {
                client.Start();
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
            client.Stop();
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

        public async void SendData(object data)
        {
            var str =JsonSerializer.Serialize(value:data);
            await client.SendAsync(Ip,Port, Encoding.UTF8.GetBytes(str));

        }
    }
}
