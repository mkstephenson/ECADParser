using Common.Models;
using Common.Models.Metadata;
using System.Data;
using System.Globalization;

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
      GetStations(lines).ForEach(s =>
      {
        if (dbContext.Stations.Find(s.StationId) == null)
        {
          dbContext.Stations.Add(s);
        }
      });
      
      dbContext.SaveChanges();
    }

    public static List<Station> GetStations(string folderPath)
    {
      return GetStations(File.ReadAllLines(Path.Combine(folderPath, "stations.txt")));
    }

    public static List<Station> GetStations(string[] lines)
    {
      var table = ParseTable(lines);
      var stations = new List<Station>();

      foreach (DataRow row in table.Rows)
      {
        stations.Add(new Station
        {
          StationId = int.TryParse(row.Field<string>("STAID"), out int sval) ? sval : 0,
          StationName = row.Field<string>("STANAME"),
          CountryCode = row.Field<string>("CN"),
          Latitude = row.Field<string>("LAT"),
          Longitude = row.Field<string>("LON"),
          Height = int.TryParse(row.Field<string>("HGHT"), out int hval) ? hval : 0
        });
      }

      return stations;
    }

    public static void AddSources(ECADContext dbContext, string folderPath)
    {
      AddSources(dbContext, File.ReadAllLines(Path.Combine(folderPath, "sources.txt")));
    }

    public static void AddSources(ECADContext dbContext, string[] lines)
    {
      GetSources(lines).ForEach(s =>
      {
        if (dbContext.Sources.Find(s.SourceId) == null)
        {
          dbContext.Sources.Add(s);
        }
      });

      dbContext.SaveChanges();
    }

    public static List<Source> GetSources(string folderPath)
    {
      return GetSources(File.ReadAllLines(Path.Combine(folderPath, "sources.txt")));
    }

    private static List<Source> GetSources(string[] lines)
    {
      var table = ParseTable(lines);
      var sources = new List<Source>();
      foreach (DataRow row in table.Rows)
      {
        sources.Add(new Source
        {
          SourceId = int.Parse(row.Field<string>("SOUID")),
          SourceName = row.Field<string>("SOUNAME"),
          CountryCode = row.Field<string>("CN"),
          Latitude = row.Field<string>("LAT"),
          Longitude = row.Field<string>("LON"),
          Height = int.TryParse(row.Field<string>("HGHT"), out var hght) ? hght : default,
          ElementId = row.Field<string>("ELEID") ?? string.Empty,
          Start = DateTime.TryParseExact(row.Field<string>("START"), "yyyyMMdd", null, DateTimeStyles.None, out var resstart) ? resstart : default,
          End = DateTime.TryParseExact(row.Field<string>("STOP"), "yyyyMMdd", null, DateTimeStyles.None, out var resend) ? resend : default,
          ParticipantId = int.TryParse(row.Field<string>("PARID"), out var parid) ? parid : default,
          ParticipantName = row.Field<string>("PARNAME") ?? string.Empty,
        });
      }
      return sources;
    }

    public static void AddElements(ECADContext dbContext, string folderPath)
    {
      AddElements(dbContext, File.ReadAllLines(Path.Combine(folderPath, "elements.txt")));
    }

    public static void AddElements(ECADContext dbContext, string[] lines)
    {
      GetElements(lines).ForEach(e =>
      {
        if (dbContext.Elements.Find(e.ElementId) == null)
        {
          dbContext.Elements.Add(e);
        }
      });

      dbContext.SaveChanges();
    }

    public static List<Element> GetElements(string folderPath)
    {
      return GetElements(File.ReadAllLines(Path.Combine(folderPath, "elements.txt")));
    }

    public static List<Element> GetElements(string[] lines)
    {
      var table = ParseTable(lines);
      var elements = new List<Element>();
      foreach (DataRow row in table.Rows)
      {
        elements.Add(new Element
        {
          ElementId = row.Field<string>("ELEID"),
          Description = row.Field<string>("DESC"),
          Unit = row.Field<string>("UNIT")
        });
      }
      return elements;
    }

    public static async Task<DataTable> ParseTableAsync(string fileName)
    {
      return ParseTable(await File.ReadAllLinesAsync(fileName));
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

          var candidate = line[startIndex..endIndex].Trim();
          if (candidate.EndsWith(','))
          {
            if (line[line.IndexOf(candidate) - 1] != ',')
            {
              startIndex -= 1;
              endIndex -= 1;
              row[column.Key] = line[startIndex..endIndex].Trim();
            }
            else
            {
              row[column.Key] = line[startIndex..endIndex].Trim(',').Trim();
            }
          }
          else
          {
            row[column.Key] = candidate;
          }
        }
        dataTable.Rows.Add(row);
      }

      return dataTable;
    }
  }
}
