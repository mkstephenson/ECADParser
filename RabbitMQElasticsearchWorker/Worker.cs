using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQElasticsearchWorker
{
  public class Worker : IHostedService
  {
    private readonly ILogger<Worker> _logger;

    private readonly string _exchangeName = "x.eca";
    private readonly string _queueName = "q.eca";
    private readonly string _routingKey = "eca";
    private IConnection _conn;
    private IModel _channel;
    private EventingBasicConsumer _consumer;

    public Worker(ILogger<Worker> logger)
    {
      _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
      var connectionFactory = new ConnectionFactory
      {
        HostName = "nas.n12.eu"
      };

      _conn = connectionFactory.CreateConnection();

      _channel = _conn.CreateModel();

      _channel.ExchangeDeclare(_exchangeName, ExchangeType.Fanout);
      _channel.QueueDeclare(_queueName, true, false, false, null);
      _channel.QueueBind(_queueName, _exchangeName, _routingKey, null);

      _consumer = new EventingBasicConsumer(_channel);
      _consumer.Received += Consumer_Received;

      _channel.BasicConsume(_queueName, true, _consumer);

      return Task.CompletedTask;
    }

    private void Consumer_Received(object? sender, BasicDeliverEventArgs e)
    {
      Console.WriteLine(Encoding.UTF8.GetString(e.Body.ToArray()));
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }
  }
}