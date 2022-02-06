using System.ComponentModel.DataAnnotations;

namespace Common.Models.Metadata
{
  public class Source
  {
    [Key]
    public int SourceId { get; set; }

    [MaxLength(40)]
    public string SourceName { get; set; }

    [MaxLength(2)]
    public string CountryCode { get; set; }

    [MaxLength(10)]
    public string Latitude { get; set; }

    [MaxLength(10)]
    public string Longitude { get; set; }

    public int Height { get; set; }

    [MaxLength(5)]
    public string ElementId { get; set; }

    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public int ParticipantId { get; set; }

    [MaxLength(50)]
    public string ParticipantName { get; set; }
  }
}
