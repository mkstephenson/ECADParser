using System.Data;

namespace Common.Models.Data
{
  public class DD : BaseElement
  {
    public DD()
    {

    }

    public DD(DataRow row) : base(row.Field<string>("STAID"), row.Field<string>("SOUID"), row.Field<string>("DATE"))
    {
      WindDirection = int.Parse(row.Field<string>("DD"));
      WindDirectionQuality = int.Parse(row.Field<string>("Q_DD"));
    }

    public int WindDirection { get; set; }
    public int WindDirectionQuality { get; set; }
  }
}
