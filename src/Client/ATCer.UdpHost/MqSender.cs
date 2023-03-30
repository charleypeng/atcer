using RabbitMQ.Client;
using System.Text;

namespace ATCer.UdpHost
{
    public class MqSender
    {
        private ConnectionFactory? _factory;
        private IModel? _channel;
        private RabbitMqOptions? _options;

        private bool failed = false;
        public string Exchange { get; }
        public MqSender(string hostName, string userName, string passWord, int port = 5672, string exchange = "logs")
        {
            _factory = new ConnectionFactory { HostName= hostName,UserName = userName, Password = passWord , Port = port };
            Exchange= exchange;
            Init();
        }

        public MqSender(RabbitMqOptions options, string exchange = "exchange.default")
        {
            if(options == null) { throw new ArgumentNullException("options"); }
            
            _options = options;

            _factory = new ConnectionFactory 
            { 
                HostName = _options.HostName, 
                UserName = _options.UserName, 
                Password = _options.Password, 
                Port = _options.Port };
            Exchange = exchange;

            Init();
        }

        
        private readonly object _syncLock = new();

        public void Init()
        {
            lock(_syncLock)
            {
                try
                {
                    using var connection = _factory?.CreateConnection();

                    _channel = connection?.CreateModel();

                    _channel?.ExchangeDeclare(exchange: Exchange, autoDelete: false, type: ExchangeType.Fanout);
                    failed = false;
                }
                catch (Exception ex)
                {
                    failed = true;
                }
            }
        }

        public Task Send(string message) 
        {
            try
            {
                using var connection = _factory?.CreateConnection();

                _channel = connection?.CreateModel();
                var body = Encoding.UTF8.GetBytes(message);
                _channel?.BasicPublish(exchange: Exchange,
                                     routingKey: string.Empty,
                                     basicProperties: null,
                                     body: body);
                failed = false;
            }
            catch (Exception)
            {
                failed = true;
            }

            return Task.CompletedTask;
        }

        public void Send(byte[] bytes)
        {
            try
            {
                using var connection = _factory?.CreateConnection();

                _channel = connection?.CreateModel();
                _channel?.BasicPublish(exchange: Exchange,
                                     routingKey: string.Empty,
                                     basicProperties: null,
                                     body: bytes);
            }
            catch (Exception)
            {
                Logger.LogInfomation("无法发送到消息队列中");
                Thread.Sleep(TimeSpan.FromSeconds(10));
                return;
            }
           
        }
    }
}
