using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelApi.Models
{
  public class Destination
  {

    public int DestinationId { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public virtual List<Review> Reviews { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int ReviewCount { get; set; } = 0;
  }
}