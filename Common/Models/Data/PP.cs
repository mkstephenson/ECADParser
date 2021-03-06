using System.Data;

namespace Common.Models.Data
{
  public class PP : BaseElement
  {
    public PP()
    {

    }

    public PP(DataRow row) : base(row.Field<string>("STAID"), row.Field<string>("SOUID"), row.Field<string>("DATE"))
    {
      SeaLevelPressure = int.Parse(row.Field<string>("PP"));
      SeaLevelPressureQuality = int.Parse(row.Field<string>("Q_PP"));
    }

    public int SeaLevelPressure { get; set; }
    public int SeaLevelPressureQuality { get; set; }
  }
}
