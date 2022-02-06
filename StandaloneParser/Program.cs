using ECADParser.Models;
using ECADParser.Models.Data;
using ECADParser.Models.Metadata;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text.Json;

var config = JsonDocument.Parse(File.ReadAllText("config.json")).RootElement;
var connectionString = config.GetProperty("ConnectionString").GetString();
var locationsConfig = config.GetProperty("DataLocations");

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
  var folder = locationsConfig.GetProperty(typeName).GetString();
  Console.WriteLine($"Parsing files in folder {folder}");
  Console.WriteLine("Adding metadata");
  AddStations(folder);
  AddSources(folder);
  AddElements(folder);

  Parallel.ForEach(Directory.EnumerateFiles(folder).Where(f => Path.GetFileName(f).StartsWith(typeName)), new ParallelOptions
  {
    MaxDegreeOfParallelism = 4
  }, file =>
  //foreach (var file in Directory.EnumerateFiles(folder).Where(f => Path.GetFileName(f).StartsWith(typeName)))
  {
    Console.WriteLine($"Adding content from file {file}");
    using var dbContext = new ECADContext(connectionString);
    var tableContent = ParseTable(file);
    foreach (DataRow row in tableContent.Rows)
    {
      addElementToCollection(row, dbContext);
    }
    dbContext.SaveChanges();
  });
}

void AddStations(string folderPath)
{
  var table = ParseTable(Path.Combine(folderPath, "stations.txt"));
  foreach (DataRow row in table.Rows)
  {
    var newStation = new Station
    {
      StationId = int.Parse(row.Field<string>("STAID")),
      StationName = row.Field<string>("STANAME"),
      CountryCode = row.Field<string>("CN"),
      Latitude = row.Field<string>("LAT"),
      Longitude = row.Field<string>("LON"),
      Height = int.Parse(row.Field<string>("HGHT"))
    };

    if (dbContext.Stations.Find(newStation.StationId) == null)
    {
      dbContext.Stations.Add(newStation);
    }
  }
  dbContext.SaveChanges();
}

void AddSources(string folderPath)
{
  var table = ParseTable(Path.Combine(folderPath, "sources.txt"));
  foreach (DataRow row in table.Rows)
  {
    var newSource = new Source
    {
      SourceId = int.Parse(row.Field<string>("SOUID")),
      SourceName = row.Field<string>("SOUNAME"),
      CountryCode = row.Field<string>("CN"),
      Latitude = row.Field<string>("LAT"),
      Longitude = row.Field<string>("LON"),
      Height = int.Parse(row.Field<string>("HGHT")),
      ElementId = row.Field<string>("ELEID"),
      Start = DateTime.ParseExact(row.Field<string>("START"), "yyyyMMdd", null),
      End = DateTime.ParseExact(row.Field<string>("STOP"), "yyyyMMdd", null),
      ParticipantId = int.Parse(row.Field<string>("PARID")),
      ParticipantName = row.Field<string>("PARNAME")
    };

    if (dbContext.Sources.Find(newSource.SourceId) == null)
    {
      dbContext.Sources.Add(newSource);
    }
  }
  dbContext.SaveChanges();
}

void AddElements(string folderPath)
{
  var table = ParseTable(Path.Combine(folderPath, "elements.txt"));
  foreach (DataRow row in table.Rows)
  {
    var newElement = new Element
    {
      ElementId = row.Field<string>("ELEID"),
      Description = row.Field<string>("DESC"),
      Unit = row.Field<string>("UNIT")
    };

    if (dbContext.Elements.Find(newElement.ElementId) == null)
    {
      dbContext.Elements.Add(newElement);
    }
  }
  dbContext.SaveChanges();
}

DataTable ParseTable(string fileName)
{
  var lines = File.ReadAllLines(fileName).SkipWhile(l => !l.StartsWith("FILE FORMAT")).Skip(2);
  var linesWithFileFormat = lines.TakeWhile(l => l != string.Empty).Select(l => l.Trim());

  Dictionary<string, int[]> columnWidths = new();

  foreach (var lf in linesWithFileFormat)
  {
    var stringsToProcess = lf.Replace("- ", "-").Split(':');
    var columnsWithData = stringsToProcess[0].Split(' ')[0].Split('-').Select(i => int.Parse(i));
    var columnName = stringsToProcess[0].Split(' ')[1];
    var description = stringsToProcess[1];
    columnWidths.Add(columnName, columnsWithData.ToArray());
  }

  var linesWithData = lines.SkipWhile(l => !columnWidths.Keys.All(d => l.Contains(d))).Skip(1).Where(l => !string.IsNullOrWhiteSpace(l));
  var dataTable = new DataTable();

  foreach (var c in columnWidths)
  {
    dataTable.Columns.Add(c.Key);
  }

  foreach (var line in linesWithData)
  {
    var row = dataTable.NewRow();
    foreach (var column in columnWidths)
    {
      var startIndex = column.Value[0] - 1;
      var length = column.Value[1] - startIndex;
      var value = line.Substring(startIndex, length);
      row[column.Key] = value.Trim();
    }
    dataTable.Rows.Add(row);
  }

  return dataTable;
}
