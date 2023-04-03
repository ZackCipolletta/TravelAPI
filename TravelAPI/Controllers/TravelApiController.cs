using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using TravelApi.Models;
using TravelApi.Wrappers;
using TravelApi.Filter;

namespace TravelApi.Controllers
{
  // [Authorize]
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
    public async Task<ActionResult<IEnumerable<Destination>>> Get([FromQuery]PaginationFilter filter, string country, string city, string search)
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
      var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

      var pagedData = await _db.Destinations.Skip((validFilter.PageNumber -1)* validFilter.PageSize).Take(validFilter.PageSize).ToListAsync();

      var totalRecords = await _db.Destinations.CountAsync();

      return Ok(new PagedResponse<List<Destination>>(pagedData,validFilter.PageNumber,validFilter.PageSize));
    }


    // GET: api/Destinations/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Destination>> GetDestination(int id)
    {
      Destination destination = await _db.Destinations.Include(d => d.Reviews).FirstOrDefaultAsync(d => d.DestinationId == id);

      if (destination == null)
      {
        return NotFound();
      }

      return Ok(new Response <Destination>(destination));
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
    [HttpPut("{id}")]
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