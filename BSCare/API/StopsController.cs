using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BSCare.Data;
using BSCare.Models;

namespace BSCare.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class StopsController : ControllerBase
    {
        private readonly BscareDbContext _context;

        public StopsController(BscareDbContext context)
        {
            _context = context;
        }

        [HttpGet("{Lat}/{Lng}/{Radius}")]
        public async Task<ActionResult<IEnumerable<Stop>>> GetStops(double Lat, double Lng, double Radius)
        {
            LatLng my_location = new LatLng(Lat, Lng);
            List<Stop> returned_stops = new List<Stop>();
            var data = await _context.Stops.ToListAsync();
            //return await _context.Stops.ToListAsync();

            foreach (var item in data)
            {
                double d = DistanceCalculation.HaversineDistance(my_location, new LatLng(item.StopLat, item.StopLon));
                if (d < Radius)
                {
                    returned_stops.Add(item);
                }
            }
            return returned_stops;
        }

        // GET: api/Stops
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stop>>> GetStops()
        {
          if (_context.Stops == null)
          {
              return NotFound();
          }
            return await _context.Stops.ToListAsync();
        }

        // GET: api/Stops/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Stop>> GetStop(int id)
        {
          if (_context.Stops == null)
          {
              return NotFound();
          }
            var stop = await _context.Stops.FindAsync(id);

            if (stop == null)
            {
                return NotFound();
            }

            return stop;
        }

        // PUT: api/Stops/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStop(int id, Stop stop)
        {
            if (id != stop.StopCode)
            {
                return BadRequest();
            }

            _context.Entry(stop).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StopExists(id))
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

        // POST: api/Stops
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Stop>> PostStop(Stop stop)
        {
          if (_context.Stops == null)
          {
              return Problem("Entity set 'BscareDbContext.Stops'  is null.");
          }
            _context.Stops.Add(stop);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StopExists(stop.StopCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetStop", new { id = stop.StopCode }, stop);
        }

        // DELETE: api/Stops/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStop(int id)
        {
            if (_context.Stops == null)
            {
                return NotFound();
            }
            var stop = await _context.Stops.FindAsync(id);
            if (stop == null)
            {
                return NotFound();
            }

            _context.Stops.Remove(stop);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StopExists(int id)
        {
            return (_context.Stops?.Any(e => e.StopCode == id)).GetValueOrDefault();
        }
    }
}
