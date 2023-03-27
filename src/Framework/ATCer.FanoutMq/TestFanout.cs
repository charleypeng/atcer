using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using DotNetCore.CAP;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace ATCer.FanoutMq
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        private ICapPublisher _publisher;
        public string QueueName { get; set; }

        public Worker(ILogger<Worker> logger, ICapPublisher publisher)
        {
            _logger = logger;
            _publisher = publisher;
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
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
            this.QueueName = _channel.QueueDeclare().QueueName;
            
            _channel.BasicQos(0, 1, false);
            _channel.QueueBind(queue: QueueName,
                    exchange: "logs",
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
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                _logger.LogInformation($"Processing msg: '{message}'.");
                try
                {
                    await _publisher.PublishAsync("testmsg", message);
                    //_logger.LogInformation($"{message}");

                    //await Task.Delay(new Random().Next(1, 3) * 1000, stoppingToken); // simulate an async email process
                  
                    _channel.BasicAck(ea.DeliveryTag, false);
                }
                catch (JsonException)
                {
                    _logger.LogError($"JSON Parse Error: '{message}'.");
                    _channel.BasicNack(ea.DeliveryTag, false, false);
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

            _channel.BasicConsume(queue: QueueName, autoAck: false, consumer: consumer);

            await Task.CompletedTask;
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
            _connection.Close();
            _logger.LogInformation("RabbitMQ connection is closed.");
        }
    }
}