﻿using Common.Models;
using Common.Models.Metadata;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
  public static class TableHelpers
  {
    public static void AddStations(ECADContext dbContext, string folderPath)
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

    public static void AddSources(ECADContext dbContext, string folderPath)
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

    public static void AddElements(ECADContext dbContext, string folderPath)
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

    public static DataTable ParseTable(string fileName)
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

  }
}