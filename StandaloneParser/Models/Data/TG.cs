using System.Data;

namespace ECADParser.Models.Data
{
  internal class TG : BaseElement
  {
    public TG()
    {

    }

    internal TG(DataRow row) : base(row.Field<string>("STAID"), row.Field<string>("SOUID"), row.Field<string>("DATE"))
    {
      MeanTemperature = int.Parse(row.Field<string>("TG"));
      MeanTemperatureQuality = int.Parse(row.Field<string>("Q_TG"));
    }

    public int MeanTemperature { get; set; }
    public int MeanTemperatureQuality { get; set; }
  }
}
