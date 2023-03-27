using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelApi.Models
{
  public class Review
  {

    public int ReviewId { get; set; }
    [ForeignKey("DestinationId")]
    public int DestinationId { get; set; }
    
    [StringLength(120)]
    public string Title { get; set; }
    public string Description { get; set; }
  }
}

