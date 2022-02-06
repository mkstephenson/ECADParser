using System.Data;

namespace ECADParser.Models.Data
{
  internal class QQ : BaseElement
  {
    public QQ()
    {

    }

    internal QQ(DataRow row) : base(row.Field<string>("STAID"), row.Field<string>("SOUID"), row.Field<string>("DATE"))
    {
      GlobalRadiation = int.Parse(row.Field<string>("QQ"));
      GlobalRadiationQuality = int.Parse(row.Field<string>("Q_QQ"));
    }

    public int GlobalRadiation { get; set; }
    public int GlobalRadiationQuality { get; set; }
  }
}
