using System.ComponentModel.DataAnnotations;

namespace ECADParser.Models.Metadata
{
  internal class Station
  {
    [Key]
    public int StationId { get; set; }

    [MaxLength(40)]
    public string StationName { get; set; }

    [MaxLength(2)]
    public string CountryCode { get; set; }

    [MaxLength(10)]
    public string Latitude { get; set; }

    [MaxLength(10)]
    public string Longitude { get; set; }

    public int Height { get; set; }
  }
}
