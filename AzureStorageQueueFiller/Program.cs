using Azure.Storage.Queues;
using System.Text.Json;

var config = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText("config.json"));
var networkSharePath = config["NetworkSharePath"];
var rootUrl = new Uri(config["HTTPRoot"]);
var queueConnectionString = config["QueueConnectionString"];
var queueName = config["QueueName"];

QueueClient queue = new QueueClient(queueConnectionString, queueName);
queue.CreateIfNotExists();

var filenames = Directory.EnumerateFiles(networkSharePath, "*.txt", SearchOption.AllDirectories).ToList();
var shuffledNames = new List<string>();
var rng = new Random();

while (filenames.Any())
{
  var index = rng.Next(filenames.Count);
  shuffledNames.Add(filenames[index]);
  filenames.RemoveAt(index);
}

Parallel.ForEach(shuffledNames, file =>
{
  var newPath = Path.GetRelativePath(networkSharePath, file);
  var url = new Uri(rootUrl, newPath);
  Console.WriteLine(url.AbsoluteUri);
  queue.SendMessage(url.AbsoluteUri);
});
