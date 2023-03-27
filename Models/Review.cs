using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelApi.Models
{
  public class Review
  {
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int ReviewId { get; set; }
    [ForeignKey("DestinationId")]
    public int DestinationId { get; set; }
    [NotMapped]
    public Destination Destination { get; set; }
    [StringLength(120)]
    public string Title { get; set; }
    public string Description { get; set; }
  }
}

