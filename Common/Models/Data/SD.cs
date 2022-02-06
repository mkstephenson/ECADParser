using System.Data;

namespace Common.Models.Data
{
  public class SD : BaseElement
  {
    public SD()
    {

    }

    public SD(DataRow row) : base(row.Field<string>("STAID"), row.Field<string>("SOUID"), row.Field<string>("DATE"))
    {
      SnowDepth = int.Parse(row.Field<string>("SD"));
      SnowDepthQuality = int.Parse(row.Field<string>("Q_SD"));
    }

    public int SnowDepth { get; set; }
    public int SnowDepthQuality { get; set; }
  }
}
