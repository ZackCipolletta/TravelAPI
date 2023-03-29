using Microsoft.EntityFrameworkCore;

namespace TravelApi.Models
{
  public class TravelApiContext : DbContext
  {
    public DbSet<Destination> Destinations { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<UserConstant> UserConstant { get; set; }
    public DbSet<UserLogin> UserLogin { get; set; }
    public DbSet<UserModel> UserModel { get; set; }
      public object Destination { get; internal set; }

    public TravelApiContext(DbContextOptions<TravelApiContext> options) : base(options)
    {
    }
  }
}