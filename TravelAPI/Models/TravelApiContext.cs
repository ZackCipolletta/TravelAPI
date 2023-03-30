using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TravelApi.Models;

namespace TravelApi.Models
{
  public class TravelApiContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<Destination> Destinations { get; set; }
    public DbSet<Review> Reviews { get; set; }

    public TravelApiContext(DbContextOptions<TravelApiContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      // Configure your model here
    }
  }
}