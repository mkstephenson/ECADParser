using System.Data;

namespace ECADParser.Models.Data
{
  internal class CC : BaseElement
  {
    public CC()
    {

    }

    internal CC(DataRow row) : base(row.Field<string>("STAID"), row.Field<string>("SOUID"), row.Field<string>("DATE"))
    {
      CloudCover = int.Parse(row.Field<string>("CC"));
      CloudCoverQuality = int.Parse(row.Field<string>("Q_CC"));
    }

    public int CloudCover { get; set; }
    public int CloudCoverQuality { get; set; }
  }
}
