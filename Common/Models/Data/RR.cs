using System.Data;

namespace Common.Models.Data
{
  public class RR : BaseElement
  {
    public RR()
    {

    }

    public RR(DataRow row) : base(row.Field<string>("STAID"), row.Field<string>("SOUID"), row.Field<string>("DATE"))
    {
      Precipitation = int.Parse(row.Field<string>("RR"));
      PrecipitationQuality = int.Parse(row.Field<string>("Q_RR"));
    }

    public int Precipitation { get; set; }
    public int PrecipitationQuality { get; set; }
  }
}
