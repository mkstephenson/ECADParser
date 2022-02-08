using Azure.Storage.Queues;
using Common.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

var config = JsonDocument.Parse(File.ReadAllText("config.json")).RootElement;
var connectionString = config.GetProperty("ConnectionString").GetString();
var queueConnectionString = config.GetProperty("QueueConnectionString").GetString();
var queueName = config.GetProperty("QueueName").GetString();

using var dbContext = new ECADContext(connectionString);
if (dbContext.Database.GetPendingMigrations().Any())
{
  dbContext.Database.Migrate();
}

QueueClient queue = new QueueClient(queueConnectionString, queueName);
queue.CreateIfNotExists();

var httpClient = new HttpClient();

while (true)
{
  var response = await queue.ReceiveMessageAsync();
  if (response.Value == null)
  {
    await Task.Delay(5000);
    continue;
  }
  else
  {
    var fileContent = await httpClient.GetAsync(response.Value.Body.ToString());
    
  }
}