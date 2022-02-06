using System.ComponentModel.DataAnnotations;

namespace ECADParser.Models.Metadata
{
  internal class Element
  {
    [Key]
    [MaxLength(5)]
    public string ElementId { get; set; }

    [MaxLength(150)]
    public string Description { get; set; }

    [MaxLength(15)]
    public string Unit { get; set; }
  }
}
