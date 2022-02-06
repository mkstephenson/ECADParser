using System.Data;

namespace ECADParser.Models.Data
{
  internal class FX : BaseElement
  {
    public FX()
    {

    }

    internal FX(DataRow row) : base(row.Field<string>("STAID"), row.Field<string>("SOUID"), row.Field<string>("DATE"))
    {
      WindGust = int.Parse(row.Field<string>("FX"));
      WindGustQuality = int.Parse(row.Field<string>("Q_FX"));
    }

    public int WindGust { get; set; }
    public int WindGustQuality { get; set; }
  }
}
