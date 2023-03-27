using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelApi.Models
{
  public class Destination
  {

    public int DestinationId { get; set; }
    public string Name { get; set; }
    public virtual List<Review> Reviews { get; set; }
  }
}