// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Text;

namespace ATCer.FanoutMq
{
    public abstract class Fanout:BackgroundService, IFanoutMq, IHostedService
    {
        private readonly object _syncLock = new();
        public string Ip { get; private set; }
        public int Port { get; private set; }
        public string? QueueName { get; private set; }
        public string? BindName { get;set; }
        public Func<TransportMsg, object?, Task>? OnMessageCallback { get; set; }
        //private fields
        private ConnectionFactory? _factory;
        private IModel? _channel;
        private IConnection? _connection;
        //AsyncEventingBasicConsumer? consumer;
        protected readonly ILogger<Fanout> _logger;
        private string _typeName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public Fanout(MqOptions options,
            ILogger<Fanout> logger)
        {
            if(options == null) throw new ArgumentNullException("options");

            if(options.Ip == null) throw new ArgumentNullException("ip");
            if(options.Port == null) throw new ArgumentNullException("port");
            if(options.BindName== null) throw new ArgumentNullException(nameof(options.BindName));

            this.Ip = options.Ip;
            this.Port = options.Port.Value;
            this.BindName = options.BindName;
            _logger = logger;
            _typeName = typeof(Fanout).Name;

            try
            {
                _factory = new ConnectionFactory
                {
                    HostName = this.Ip,
                    Port = this.Port,
                    UserName = options.Username,
                    Password = options.Password,
                    DispatchConsumersAsync = true,
                    AutomaticRecoveryEnabled = true,
                    TopologyRecoveryEnabled = true
                };
            }
            catch (Exception ex)
            {
                logger.LogError("无法初始化工厂程序", ex);
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"开始启动{_typeName}");

            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ConsumerCancelled += Consumer_ConsumerCancelled;
            consumer.Registered += Consumer_Registered;
            consumer.Unregistered += Consumer_Unregistered;

            //consumer.Received += Consumer_Received;
            consumer.Received += async(bc, ea) =>
            {
#if DEBUG
                _logger.LogWarning($"收到消息：{Encoding.UTF8.GetString(ea.Body.ToArray())}");
#endif
                try
                {
                    var message = new TransportMsg(ea.Body);
                    //send message
                    await OnMessageCallback!(message, ea.DeliveryTag);
                    //acknowledge message
                    _channel?.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    
                }
                catch (AlreadyClosedException)
                {
                    _logger.LogInformation("RabbitMQ 已经关闭!");
                }
                catch (Exception e)
                {
                    _logger.LogError(default, e, e.Message);
                }
                
            };
            //_channel?.BasicQos(0, 1, false);
            _channel?.BasicConsume(queue: QueueName,
                                 consumer: consumer, autoAck: false);


            return Task.CompletedTask;
        }

        private void tryDeclareQueue()
        {
            _connection = _factory?.CreateConnection();
            try
            {
                lock (_syncLock)
                {
                    if(_channel == null || _channel.IsClosed)
                    {
                        _channel = _connection?.CreateModel();
                        tryDeclareExchange();
                        if (string.IsNullOrWhiteSpace(QueueName))
                        {
                            this.QueueName = _channel.QueueDeclare().QueueName;
                        }
                        else
                        {
                            _channel.QueueDeclare(QueueName, false);
                        }
                        _channel.QueueBind(queue: QueueName,
                            exchange: BindName,
                            routingKey: string.Empty);
                    }
                    _logger.LogInformation($"Queue [{QueueName}] is waiting for messages.");
                }
                
            }
            catch (Exception)
            {
                tryDeclareExchange();
                throw;
            }
            
        }
        private void tryDeclareExchange()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(BindName))
                {
                    _channel.ExchangeDeclare(exchange: BindName, type: ExchangeType.Fanout, autoDelete: false);
                }
            }
            catch (Exception)
            {
                _logger.LogInformation($"{BindName} creation failed");
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

        //private async Task Consumer_Received(object? sender, BasicDeliverEventArgs e)
        //{
        //    var message = new TransportMsg(e.Body);
        //    //await OnMessage!.InvokeAsync(this, new FanoutEventArgs(message));
        //    _channel?.BasicAck(deliveryTag: e.DeliveryTag, multiple: false);
        //    //await OnMessageCallback!(message, e.DeliveryTag);
        //}
        private Task Consumer_ConsumerCancelled(object sender, ConsumerEventArgs @event)
        {
            _logger.LogWarning($"consumer registed:{@event.ConsumerTags}");
            return Task.CompletedTask;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            tryDeclareQueue();
            return base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogWarning($"{_typeName} 已经停止");
            await base.StopAsync(cancellationToken);
            _channel?.Close();
            _channel?.Dispose();
            _logger.LogInformation("FanoutMq connection is closed");
        }
        #endregion
    }
}