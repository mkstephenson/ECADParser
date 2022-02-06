﻿using System.Data;

namespace ECADParser.Models.Data
{
  internal class HU : BaseElement
  {
    public HU()
    {

    }

    internal HU(DataRow row) : base(row.Field<string>("STAID"), row.Field<string>("SOUID"), row.Field<string>("DATE"))
    {
      Humidity = int.Parse(row.Field<string>("HU"));
      HumidityQuality = int.Parse(row.Field<string>("Q_HU"));
    }

    public int Humidity { get; set; }
    public int HumidityQuality { get; set; }
  }
}
