using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using TravelApi.Models;

namespace TravelApi.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class DestinationsController : ControllerBase
  {
    private readonly TravelApiContext _db;

    public DestinationsController(TravelApiContext db)
    {
      _db = db;
    }

    // GET api/destinations
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Destination>>> Get(string country, string city, string search)
    {
      IQueryable<Destination> query = _db.Destinations.Include(destination => destination.Reviews).AsQueryable();

      if (country != null)
      {
        query = query.Where(entry => entry.Country == country);
      }

      if (city != null)
      {
        query = query.Where(entry => entry.City == city);
      }

      if (search == "random")
      {
        Random random = new Random();
        int randomId = random.Next(1, (1 + query.Count()));
        query = query.Where(entry => entry.DestinationId == randomId);
      }

      var destinations = await query.ToListAsync();

      foreach (var destination in destinations)
      {
        destination.ReviewCount = destination.Reviews?.Count() ?? 0;
      }

      if (search == "popular")
      {
        destinations = destinations.OrderByDescending(destinations => destinations.ReviewCount).ToList();
      }

      return destinations;
    }


    // GET: api/Destinations/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Destination>> GetDestination(int id)
    {
      Destination destination = await _db.Destinations.FindAsync(id);

      if (destination == null)
      {
        return NotFound();
      }

      return destination;
    }

    // POST api/destinations
    [HttpPost]
    public async Task<ActionResult<Destination>> Post(Destination destination)
    {
      _db.Destinations.Add(destination);
      await _db.SaveChangesAsync();
      return CreatedAtAction(nameof(GetDestination), new { id = destination.DestinationId }, destination);
    }


    // PUT: api/Destinations/5
    [HttpPut("destinations/{id}")]
    public async Task<IActionResult> Put(int id, Destination destination)
    {
      if (id != destination.DestinationId)
      {
        return BadRequest();
      }

      _db.Destinations.Update(destination);

      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!DestinationExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    private bool DestinationExists(int id)
    {
      return _db.Destinations.Any(e => e.DestinationId == id);
    }

    // _______________________________________________________________________________
    // PUT: api/Reviews/5
    // [HttpPut("reviews/{id}")]
    // public async Task<IActionResult> Put(int id, [FromBody] Review review)
    // {
    //   if (id != review.ReviewId)
    //   {
    //     return BadRequest();
    //   }

    //   _db.Reviews.Update(review);

    //   try
    //   {
    //     await _db.SaveChangesAsync();
    //   }
    //   catch (DbUpdateConcurrencyException)
    //   {
    //     if (!ReviewExists(id))
    //     {
    //       return NotFound();
    //     }
    //     else
    //     {
    //       throw;
    //     }
    //   }

    //   return NoContent();
    // }

    // private bool ReviewExists(int id)
    // {
    //   return _db.Reviews.Any(e => e.ReviewId == id);
    // }



[HttpPut("reviews/{id}")]
public async Task<IActionResult> Put(int id, [FromBody] Review review)
{
    if (id != review.ReviewId)
    {
        return BadRequest();
    }

    Review existingReview = await _db.Reviews.FindAsync(id);

    if (existingReview == null)
    {
        return NotFound();
    }

    if (existingReview.user_name != review.user_name)
    {
        return BadRequest("User name does not match.");
    }

    // Update non-user name fields
    existingReview.Title = review.Title;
    existingReview.Description = review.Description;

    try
    {
        await _db.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!ReviewExists(id))
        {
            return NotFound();
        }
        else
        {
            throw;
        }
    }

    return NoContent();
}

private bool ReviewExists(int id)
{
    return _db.Reviews.Any(e => e.ReviewId == id);
}


























    // _________________________________________________________________
    // DELETE: api/Destinations/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDestination(int id)
    {
      Destination destination = await _db.Destinations.FindAsync(id);
      if (destination == null)
      {
        return NotFound();
      }

      _db.Destinations.Remove(destination);
      await _db.SaveChangesAsync();

      return NoContent();
    }
  }
}