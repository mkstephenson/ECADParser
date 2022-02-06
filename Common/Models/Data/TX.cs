using System.Data;

namespace Common.Models.Data
{
  public class TX : BaseElement
  {
    public TX()
    {

    }

    public TX(DataRow row) : base(row.Field<string>("STAID"), row.Field<string>("SOUID"), row.Field<string>("DATE"))
    {
      MaxTemperature = int.Parse(row.Field<string>("TX"));
      MaxTemperatureQuality = int.Parse(row.Field<string>("Q_TX"));
    }

    public int MaxTemperature { get; set; }
    public int MaxTemperatureQuality { get; set; }
  }
}
