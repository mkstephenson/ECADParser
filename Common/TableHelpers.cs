using Common.Models;
using Common.Models.Metadata;
using System.Data;

namespace Common
{
  public static class TableHelpers
  {
    public static void AddStations(ECADContext dbContext, string folderPath)
    {
      AddStations(dbContext, File.ReadAllLines(Path.Combine(folderPath, "stations.txt")));
    }

    public static void AddStations(ECADContext dbContext, string[] lines)
    {
      var table = ParseTable(lines);
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

    public static void AddSources(ECADContext dbContext, string folderPath)
    {
      AddSources(dbContext, File.ReadAllLines(Path.Combine(folderPath, "sources.txt")));
    }

    public static void AddSources(ECADContext dbContext, string[] lines)
    {
      var table = ParseTable(lines);
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

    public static void AddElements(ECADContext dbContext, string folderPath)
    {
      AddElements(dbContext, File.ReadAllLines(Path.Combine(folderPath, "elements.txt")));
    }

    public static void AddElements(ECADContext dbContext, string[] lines)
    {
      var table = ParseTable(lines);
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

    public static DataTable ParseTable(string fileName)
    {
      return ParseTable(File.ReadAllLines(fileName));
    }

    public static DataTable ParseTable(string[] inputLines)
    {
      var lines = inputLines.SkipWhile(l => !l.StartsWith("FILE FORMAT")).Skip(2);
      var linesWithFileFormat = lines.TakeWhile(l => l != string.Empty).Select(l => l.Trim()).ToList();

      Dictionary<string, int[]> columnWidths = new();

      foreach (var lf in linesWithFileFormat)
      {
        var stringsToProcess = lf.Replace("- ", "-").Split(':');
        var columnsWithData = stringsToProcess[0].Split(' ')[0].Split('-').Select(i => int.Parse(i));
        var columnName = stringsToProcess[0].Split(' ')[1];
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
          var endIndex = column.Value[1];
          if (endIndex > line.Length)
          {
            break;
          }

          row[column.Key] = line[startIndex..endIndex].Trim().Trim(',');
        }
        dataTable.Rows.Add(row);
      }

      return dataTable;
    }
  }
}
