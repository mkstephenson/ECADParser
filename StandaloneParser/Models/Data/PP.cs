using System.Data;

namespace ECADParser.Models.Data
{
  internal class PP : BaseElement
  {
    public PP()
    {

    }

    internal PP(DataRow row) : base(row.Field<string>("STAID"), row.Field<string>("SOUID"), row.Field<string>("DATE"))
    {
      SeaLevelPressure = int.Parse(row.Field<string>("PP"));
      SeaLevelPressureQuality = int.Parse(row.Field<string>("Q_PP"));
    }

    public int SeaLevelPressure { get; set; }
    public int SeaLevelPressureQuality { get; set; }
  }
}
