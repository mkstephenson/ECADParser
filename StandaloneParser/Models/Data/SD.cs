using System.Data;

namespace ECADParser.Models.Data
{
  internal class SD : BaseElement
  {
    public SD()
    {

    }

    internal SD(DataRow row) : base(row.Field<string>("STAID"), row.Field<string>("SOUID"), row.Field<string>("DATE"))
    {
      SnowDepth = int.Parse(row.Field<string>("SD"));
      SnowDepthQuality = int.Parse(row.Field<string>("Q_SD"));
    }

    public int SnowDepth { get; set; }
    public int SnowDepthQuality { get; set; }
  }
}
