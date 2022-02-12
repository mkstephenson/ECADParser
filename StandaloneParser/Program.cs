using Common;
using Common.Models;
using Common.Models.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text.Json;

var configDoc = JsonDocument.Parse(File.ReadAllText("config.json"));

var connectionString = configDoc.RootElement.GetProperty("ConnectionString").GetString();
var locationsConfig = JsonSerializer.Deserialize<Dictionary<string, string>>(configDoc.RootElement.GetProperty("DataLocations"));

using var dbContext = new ECADContext(connectionString);
if (dbContext.Database.GetPendingMigrations().Any())
{
  dbContext.Database.Migrate();
}

/*
 * Cloud Cover
 */
PopulateCollection("CC", (row, context) => context.CC.Add(new CC(row)));

/*
 * Wind Direction
 */
PopulateCollection("DD", (row, context) => context.DD.Add(new DD(row)));

/*
 * Wind Speed
 */
PopulateCollection("FG", (row, context) => context.FG.Add(new FG(row)));

/*
 * Wind Gust
 */
PopulateCollection("FX", (row, context) => context.FX.Add(new FX(row)));

/*
 * Humidity
 */
PopulateCollection("HU", (row, context) => context.HU.Add(new HU(row)));

/*
 * Sea Level Pressure
 */
PopulateCollection("PP", (row, context) => context.PP.Add(new PP(row)));

/*
 * Global Radiation
 */
PopulateCollection("QQ", (row, context) => context.QQ.Add(new QQ(row)));

/*
 * Precipitation
 */
PopulateCollection("RR", (row, context) => context.RR.Add(new RR(row)));

/*
 * Snow Depth
 */
PopulateCollection("SD", (row, context) => context.SD.Add(new SD(row)));

/*
 * Sunshine
 */
PopulateCollection("SS", (row, context) => context.SS.Add(new SS(row)));

/*
 * Mean Temperature
 */
PopulateCollection("TG", (row, context) => context.TG.Add(new TG(row)));

/*
 * Minimum Temperature
 */
PopulateCollection("TN", (row, context) => context.TN.Add(new TN(row)));

/*
 * Maximum Temperature
 */
PopulateCollection("TX", (row, context) => context.TX.Add(new TX(row)));

Console.WriteLine("Finished parsing values");

/*
 * Helper Methods
 */
void PopulateCollection(string typeName, Action<DataRow, ECADContext> addElementToCollection)
{
  var folder = locationsConfig[typeName];
  Console.WriteLine($"Parsing files in folder {folder}");
  Console.WriteLine("Adding metadata");
  TableHelpers.AddStations(dbContext, folder);
  TableHelpers.AddSources(dbContext, folder);
  TableHelpers.AddElements(dbContext, folder);

  Parallel.ForEach(Directory.EnumerateFiles(folder).Where(f => Path.GetFileName(f).StartsWith(typeName)), new ParallelOptions
  {
    MaxDegreeOfParallelism = Environment.ProcessorCount
  }, file =>
  {
    Console.WriteLine($"Adding content from file {file}");
    using var dbContext = new ECADContext(connectionString);
    var tableContent = TableHelpers.ParseTable(file);
    foreach (DataRow row in tableContent.Rows)
    {
      addElementToCollection(row, dbContext);
    }
    dbContext.SaveChanges();
  });
}
