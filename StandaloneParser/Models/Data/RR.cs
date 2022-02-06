using System.Data;

namespace ECADParser.Models.Data
{
  internal class RR : BaseElement
  {
    public RR()
    {

    }

    internal RR(DataRow row) : base(row.Field<string>("STAID"), row.Field<string>("SOUID"), row.Field<string>("DATE"))
    {
      Precipitation = int.Parse(row.Field<string>("RR"));
      PrecipitationQuality = int.Parse(row.Field<string>("Q_RR"));
    }

    public int Precipitation { get; set; }
    public int PrecipitationQuality { get; set; }
  }
}
