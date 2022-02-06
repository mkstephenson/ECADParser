using System.Data;

namespace Common.Models.Data
{
  public class TN : BaseElement
  {
    public TN()
    {

    }

    public TN(DataRow row) : base(row.Field<string>("STAID"), row.Field<string>("SOUID"), row.Field<string>("DATE"))
    {
      MinTemperature = int.Parse(row.Field<string>("TN"));
      MinTemperatureQuality = int.Parse(row.Field<string>("Q_TN"));
    }

    public int MinTemperature { get; set; }
    public int MinTemperatureQuality { get; set; }
  }
}
