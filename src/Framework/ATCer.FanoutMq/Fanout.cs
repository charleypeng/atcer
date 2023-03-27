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
using System.Text.Json;
using System.Threading.Channels;

namespace ATCer.FanoutMq
{
    public abstract class Fanout:BackgroundService, IConsumer, IHostedService
    {
        public string Ip { get; private set; }
        public int Port { get; private set; }
        public string? QueueName { get; private set; }
        public string? BindName { get; private set; }
        public Func<TransportMsg, object?, Task>? OnMessageCallback { get; set; }
        //private fields
        private ConnectionFactory? factory;
        private IModel? channel;
        AsyncEventingBasicConsumer? consumer;
        protected readonly ILogger<Fanout> _logger;

        public event AsyncEventHandler<FanoutEventArgs>? OnMessage;
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

            try
            {
                factory = new ConnectionFactory
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
                logger.LogError("无法创建连接", ex);
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogError("执行");
            stoppingToken.ThrowIfCancellationRequested();
            consumer = new AsyncEventingBasicConsumer(channel);

            //consumer.Received += Consumer_Received;
            consumer.Received +=  (bc, ea) =>
            {
                _logger.LogWarning(Encoding.UTF8.GetString(ea.Body.ToArray()));
                return Task.CompletedTask;
            };
            consumer.ConsumerCancelled += Consumer_ConsumerCancelled;
            consumer.Registered += Consumer_Registered;
            consumer.Unregistered += Consumer_Unregistered;

            channel.BasicConsume(queue: QueueName,
                                 consumer: consumer, autoAck: false);
            return Task.CompletedTask;
        }

        private void tryDeclareQueue()
        {
            tryDeclareExchange();
            try
            {
                using var connection = factory?.CreateConnection();
                channel = connection?.CreateModel();
                if (string.IsNullOrWhiteSpace(QueueName))
                {
                    this.QueueName = channel.QueueDeclare().QueueName;
                }
                channel?.BasicQos(0, 1, false);
                channel.QueueBind(queue: QueueName,
                    exchange: BindName,
                    routingKey: string.Empty);
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

                    using var connection = factory?.CreateConnection();
                    channel = connection?.CreateModel();

                    channel.ExchangeDeclare(exchange: BindName, type: ExchangeType.Fanout, autoDelete: false);
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

        private async Task Consumer_Received(object? sender, BasicDeliverEventArgs e)
        {
            var message = new TransportMsg(e.Body);
            await OnMessage!.InvokeAsync(this, new FanoutEventArgs(message));
            channel?.BasicAck(deliveryTag: e.DeliveryTag, multiple: false);
            //await OnMessageCallback!(message, e.DeliveryTag);
        }
        private Task Consumer_ConsumerCancelled(object sender, ConsumerEventArgs @event)
        {
            _logger.LogWarning($"consumer registed:{@event.ConsumerTags}");
            return Task.CompletedTask;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogError("开始");
            tryDeclareQueue();
            return base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogError("结束");
            await base.StopAsync(cancellationToken);
            channel?.Close();
            _logger.LogInformation("FanoutMq connection is closed");
        }
        #endregion
    }
}