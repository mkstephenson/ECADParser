using System.Data;

namespace Common.Models.Data
{
  public class FG : BaseElement
  {
    public FG()
    {

    }

    public FG(DataRow row) : base(row.Field<string>("STAID"), row.Field<string>("SOUID"), row.Field<string>("DATE"))
    {
      WindSpeed = int.Parse(row.Field<string>("FG"));
      WindSpeedQuality = int.Parse(row.Field<string>("Q_FG"));
    }

    public int WindSpeed { get; set; }
    public int WindSpeedQuality { get; set; }
  }
}
