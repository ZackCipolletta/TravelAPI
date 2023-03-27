using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelApi.Models
{
  public class Destination
  {
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int DestinationId { get; set; }
    public string Name { get; set; }
    public List<Review> Reviews { get; set; }
  }
}