using System.Data;

namespace ECADParser.Models.Data
{
  internal class SS : BaseElement
  {
    public SS()
    {

    }

    internal SS(DataRow row) : base(row.Field<string>("STAID"), row.Field<string>("SOUID"), row.Field<string>("DATE"))
    {
      Sunshine = int.Parse(row.Field<string>("SS"));
      SunshineQuality = int.Parse(row.Field<string>("Q_SS"));
    }

    public int Sunshine { get; set; }
    public int SunshineQuality { get; set; }
  }
}
