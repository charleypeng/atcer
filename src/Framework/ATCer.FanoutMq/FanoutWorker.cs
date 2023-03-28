using System.Text;
using Consul;
using DotNetCore.CAP;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace ATCer.FanoutMq
{
    public abstract class FanoutWorker : BackgroundService
    {
        private readonly ILogger<FanoutWorker> _logger;
        private ConnectionFactory? _connectionFactory;
        private IConnection? _connection;
        private IModel? _channel;
        //protected readonly ICapPublisher _publisher;
        public string? QueueName { get;private set; }
        protected readonly string _bindName;
        protected readonly string _topic;
        public Func<TransportMsg,object?,Task>? Worker { get; set; }
        public FanoutWorker(string bindName, string topic,ILogger<FanoutWorker> logger )//, ICapPublisher publisher)
        {
            _logger = logger;
            //_publisher = publisher;
            _bindName = bindName;
            _topic = topic;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = "189.161.20.111",
                Port = 5672,
                UserName = "admin",
                Password = "1qaz@WSX3edc",
                DispatchConsumersAsync = true
            };
            _connection = _connectionFactory?.CreateConnection();
            _channel = _connection?.CreateModel();
            this.QueueName = _channel?.QueueDeclare(autoDelete:true,durable:false).QueueName;
            
            _channel?.BasicQos(0, 1, false);
            _channel?.QueueBind(queue: QueueName,
                    exchange: _bindName,
                    routingKey: string.Empty);
            _logger.LogInformation($"Queue [{QueueName}] is waiting for messages.");

            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += async (bc, ea) =>
            {
                try
                {
                    var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                    var message1 = new TransportMsg(ea.Body);
                    await Worker!(message1, ea.DeliveryTag);
                    _logger.LogInformation($"{message}");

                    //await Task.Delay(new Random().Next(1, 3) * 1000, stoppingToken); // simulate an async email process

                    _channel?.BasicAck(ea.DeliveryTag, false);
                }
                catch (AlreadyClosedException)
                {
                    _logger.LogInformation("RabbitMQ is closed!");
                }
                catch (Exception e)
                {
                    _logger.LogError(default, e, e.Message);
                }
            };

            _channel?.BasicConsume(queue: QueueName, autoAck: false, consumer: consumer);

            await Task.CompletedTask;
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
            _connection?.Close();
            _logger.LogInformation("RabbitMQ connection is closed.");
        }
    }
}