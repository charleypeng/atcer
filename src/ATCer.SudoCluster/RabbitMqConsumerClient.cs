// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ATCer.SudoCluster
{
    public class RabbitMqConsumerClient:IComsumer
    {
        private readonly object _syncLock = new();

        private IConnectionFactory? _factory;
        private readonly RabbitMQOptions _rabbitMqOptions;
        private IModel? _channel;
        private readonly string _queueName;
        private readonly string _exchangeName;
        private readonly ILogger<RabbitMqConsumerClient> _logger;
        public RabbitMqConsumerClient(string queueName,
            ILogger<RabbitMqConsumerClient> logger,
            IOptions<RabbitMQOptions> options)
        {
            _logger = logger;
            _queueName = queueName;
            _rabbitMqOptions = options.Value;
            _exchangeName = _rabbitMqOptions.ExchangeName;
        }

        public Func<TransportMessage, object?, Task?>? OnMsgCallBack { get; set; }
        
        public void Listening(TimeSpan timeout, CancellationToken cancellationToken)
        {
            Connect();

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.Received += Consumer_Received;
            consumer.Shutdown += Consumer_Shutdown;
            consumer.Registered += Consumer_Registered;
            consumer.Unregistered += Consumer_Unregistered;
            consumer.ConsumerCancelled += Consumer_ConsumerCancelled;

            if (_rabbitMqOptions.BasicQosOptions != null)
                _channel?.BasicQos(0, _rabbitMqOptions.BasicQosOptions.PrefetchCount, _rabbitMqOptions.BasicQosOptions.Global);

            _channel.BasicConsume(_queueName, false, consumer);

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();
                cancellationToken.WaitHandle.WaitOne(timeout);
            }
        }

        public void Connect()
        {
            _factory = new ConnectionFactory { HostName = _rabbitMqOptions.HostName,
                Port = _rabbitMqOptions.Port,
                UserName = _rabbitMqOptions.UserName,
                Password = _rabbitMqOptions.Password};

            var connection = _factory.CreateConnection();

            _channel = connection.CreateModel();

            lock (_syncLock)
            {
                if (_channel == null || _channel.IsClosed)
                {
                    _channel = connection.CreateModel();

                    _channel.ExchangeDeclare(_exchangeName, _rabbitMqOptions?.ExchangeType, true);

                    //var arguments = new Dictionary<string, object>
                    //{
                    //    {"x-message-ttl", _rabbitMqOptions?.QueueArguments.MessageTTL}
                    //};

                    //if (!string.IsNullOrEmpty(_rabbitMqOptions.QueueArguments.QueueMode))
                    //{
                    //    arguments.Add("x-queue-mode", _rabbitMqOptions.QueueArguments.QueueMode);
                    //}

                    //if (!string.IsNullOrEmpty(_rabbitMqOptions.QueueArguments.QueueType))
                    //{
                    //    arguments.Add("x-queue-type", _rabbitMqOptions.QueueArguments.QueueType);
                    //}

                    _channel.QueueDeclare(_queueName, durable: true, exclusive: false, autoDelete: false); //, arguments: arguments);
                }
            }
        }

        #region consumer events
        private Task Consumer_Unregistered(object sender, ConsumerEventArgs @event)
        {
            _logger.LogWarning($"consumer unregistered:{@event.ConsumerTags}");
            return Task.CompletedTask;
        }

        private Task Consumer_Registered(object sender, ConsumerEventArgs @event)
        {
            _logger.LogWarning($"consumer registed:{@event.ConsumerTags}");
            return Task.CompletedTask;
        }

        private Task Consumer_Shutdown(object sender, ShutdownEventArgs @event)
        {
            _logger.LogWarning($"consumer registed:{@event.ReplyText}");
            return Task.CompletedTask;
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            var message = new TransportMessage(@event.Body);

            await OnMsgCallBack(message, @event.DeliveryTag)!;
        }
        private Task Consumer_ConsumerCancelled(object sender, ConsumerEventArgs @event)
        {
            _logger.LogWarning($"consumer registed:{@event.ConsumerTags}");
            return Task.CompletedTask;
        }
        #endregion
    }
}
