using RabbitMQ.Client;
using System.Text;

var exchangeName = "x.eca";
var queueName = "q.eca";
var routingKey = "eca";

var connectionFactory = new ConnectionFactory();
connectionFactory.HostName = "nas.n12.eu";
var conn = connectionFactory.CreateConnection();

var channel = conn.CreateModel();

channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout);
channel.QueueDeclare(queueName, true, false, false, null);
channel.QueueBind(queueName, exchangeName, routingKey, null);

foreach (var file in Directory.EnumerateFiles("\\\\nas\\web\\eca-json"))
{
  var name = Path.GetFileName(file);
  var content = new ReadOnlyMemory<byte>(Encoding.UTF8.GetBytes(Path.Combine("http://nas.n12.eu/eca-json/", name)));
  channel.BasicPublish(exchangeName, routingKey, null, content);
}
