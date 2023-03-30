using Microsoft.EntityFrameworkCore;

namespace TravelApi.Models
{
  public class TravelApiContext : DbContext
  {
    public DbSet<Destination> Destinations { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<UserLogin> UserLogin { get; set; }
    public DbSet<ApplicationUser> ApplicationUser { get; set; }
    public object Destination { get; internal set; }

    public TravelApiContext(DbContextOptions<TravelApiContext> options) : base(options)
    {
    }
  }
}

    // protected override void OnModelCreating(ModelBuilder builder)
    // {
    //   Destination destination = new Destination {Name = "North Korea"}; builder.Entity<Destination>()
    //     .HasData(destination);
        
        //   new Destination { DestinationId = 2, Name = "Syria"},
        //   new Destination { DestinationId = 3, Name = "Iran"},
        //   new Destination { DestinationId = 4, Name = "Russia"},
        //   new Destination { DestinationId = 5, Name = "Ukraine" }
        // );

      // builder.Entity<Review>()
      //   .HasData(
      //     new {Title = "North Korea", Description = "Words Would go here", Destination = destination});
        //   new Review { ReviewId = 2, DestinationId = 2, Title = "Syria", Description = "Words Would go here"},
        //   new Review { ReviewId = 3, DestinationId = 3, Title = "Iran", Description = "Words Would go here"},
        //   new Review { ReviewId = 4, DestinationId = 4, Title = "Russia", Description = "Words Would go here"},
        //   new Review { ReviewId = 5, DestinationId = 5, Title = "Ukraine", Description = "Words Would go here"}
        // );