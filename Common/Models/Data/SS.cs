using System.Data;

namespace Common.Models.Data
{
  public class SS : BaseElement
  {
    public SS()
    {

    }

    public SS(DataRow row) : base(row.Field<string>("STAID"), row.Field<string>("SOUID"), row.Field<string>("DATE"))
    {
      Sunshine = int.Parse(row.Field<string>("SS"));
      SunshineQuality = int.Parse(row.Field<string>("Q_SS"));
    }

    public int Sunshine { get; set; }
    public int SunshineQuality { get; set; }
  }
}
