using Azure.Storage.Queues;
using Common;
using Common.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text.Json;

var config = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText("config.json"));
var connectionString = config["ConnectionString"];
var queueConnectionString = config["QueueConnectionString"];
var queueName = config["QueueName"];

using (var dbContext = new ECADContext(connectionString))
{
  if (dbContext.Database.GetPendingMigrations().Any())
  {
    dbContext.Database.Migrate();
  }
}

QueueClient queue = new QueueClient(queueConnectionString, queueName);
queue.CreateIfNotExists();

List<Task> backgroundTasks = new List<Task>();

for (int i = 0; i < Environment.ProcessorCount; i++)
{
  backgroundTasks.Add(Task.Run(async () =>
  {
    var httpClient = new HttpClient();
    while (true)
    {
      var response = await queue.ReceiveMessageAsync();
      if (response.Value == null)
      {
        Console.WriteLine("No messages found, waiting 5 seconds");
        await Task.Delay(5000);
        continue;
      }
      else
      {
        Console.WriteLine("Message found, processing");
        using var dbContext = new ECADContext(connectionString);
        var path = response.Value.Body.ToString();
        var fileName = Path.GetFileName(path);
        Console.WriteLine($"File to download is {path}");
        using var fileContent = await httpClient.GetAsync(path);
        if (fileContent.IsSuccessStatusCode)
        {
          var lines = await fileContent.Content.ReadAsStringAsync();
          Console.WriteLine($"Total string size is {lines.Length}");
          if (fileName == "stations.txt")
          {
            TableHelpers.AddStations(dbContext, lines);
          }
          else if (fileName == "sources.txt")
          {
            TableHelpers.AddSources(dbContext, lines);
          }
          else if (fileName == "elements.txt")
          {
            TableHelpers.AddElements(dbContext, lines);
          }
          else
          {
            var splitLines = lines.Split('\n').Select(l => l.Trim('\r')).ToArray();
            var table = TableHelpers.ParseTable(splitLines);

            foreach (DataRow row in table.Rows)
            {
              if (fileName.StartsWith("CC"))
              {
                dbContext.CC.Add(new Common.Models.Data.CC(row));
              }
              else if (fileName.StartsWith("DD"))
              {
                dbContext.DD.Add(new Common.Models.Data.DD(row));
              }
              else if (fileName.StartsWith("FG"))
              {
                dbContext.FG.Add(new Common.Models.Data.FG(row));
              }
              else if (fileName.StartsWith("FX"))
              {
                dbContext.FX.Add(new Common.Models.Data.FX(row));
              }
              else if (fileName.StartsWith("HU"))
              {
                dbContext.HU.Add(new Common.Models.Data.HU(row));
              }
              else if (fileName.StartsWith("PP"))
              {
                dbContext.PP.Add(new Common.Models.Data.PP(row));
              }
              else if (fileName.StartsWith("QQ"))
              {
                dbContext.QQ.Add(new Common.Models.Data.QQ(row));
              }
              else if (fileName.StartsWith("RR"))
              {
                dbContext.RR.Add(new Common.Models.Data.RR(row));
              }
              else if (fileName.StartsWith("SD"))
              {
                dbContext.SD.Add(new Common.Models.Data.SD(row));
              }
              else if (fileName.StartsWith("SS"))
              {
                dbContext.SS.Add(new Common.Models.Data.SS(row));
              }
              else if (fileName.StartsWith("TG"))
              {
                dbContext.TG.Add(new Common.Models.Data.TG(row));
              }
              else if (fileName.StartsWith("TN"))
              {
                dbContext.TN.Add(new Common.Models.Data.TN(row));
              }
              else if (fileName.StartsWith("TX"))
              {
                dbContext.TX.Add(new Common.Models.Data.TX(row));
              }
            }
            dbContext.SaveChanges();
          }
          await queue.DeleteMessageAsync(response.Value.MessageId, response.Value.PopReceipt);
        }
        Console.WriteLine("Processed message");
      }
    }
  }));
}

Task.WaitAll(backgroundTasks.ToArray());