using System.Data;

namespace Common.Models.Data
{
  public class TG : BaseElement
  {
    public TG()
    {

    }

    public TG(DataRow row) : base(row.Field<string>("STAID"), row.Field<string>("SOUID"), row.Field<string>("DATE"))
    {
      MeanTemperature = int.Parse(row.Field<string>("TG"));
      MeanTemperatureQuality = int.Parse(row.Field<string>("Q_TG"));
    }

    public int MeanTemperature { get; set; }
    public int MeanTemperatureQuality { get; set; }
  }
}
