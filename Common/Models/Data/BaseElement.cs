using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models.Data
{
  public class BaseElement
  {
    public BaseElement()
    {

    }

    public BaseElement(string stationId, string sourceId, string date)
    {
      StationId = int.Parse(stationId);
      SourceId = int.Parse(sourceId);
      Date = DateTime.ParseExact(date, "yyyyMMdd", null);
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }
    public int StationId { get; set; }
    public int SourceId { get; set; }
    public DateTime Date { get; set; }
  }
}
