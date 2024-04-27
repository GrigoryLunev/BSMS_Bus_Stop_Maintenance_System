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
    public class RepairsController : ControllerBase
    {
        private readonly BscareDbContext _context;

        public RepairsController(BscareDbContext context)
        {
            _context = context;
        }

        // GET: api/Repairs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Repair>>> GetRepairs()
        {
          if (_context.Repairs == null)
          {
              return NotFound();
          }
            return await _context.Repairs.ToListAsync();
        }

        // GET: api/Repairs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Repair>> GetRepair(int id)
        {
          if (_context.Repairs == null)
          {
              return NotFound();
          }
            var repair = await _context.Repairs.FindAsync(id);

            if (repair == null)
            {
                return NotFound();
            }

            return repair;
        }

        // PUT: api/Repairs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRepair(int id, Repair repair)
        {
            if (id != repair.ExpensesId)
            {
                return BadRequest();
            }

            _context.Entry(repair).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RepairExists(id))
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

        // POST: api/Repairs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Repair>> PostRepair(Repair repair)
        {
          if (_context.Repairs == null)
          {
              return Problem("Entity set 'BscareDbContext.Repairs'  is null.");
          }
            _context.Repairs.Add(repair);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRepair", new { id = repair.ExpensesId }, repair);
        }

        // DELETE: api/Repairs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRepair(int id)
        {
            if (_context.Repairs == null)
            {
                return NotFound();
            }
            var repair = await _context.Repairs.FindAsync(id);
            if (repair == null)
            {
                return NotFound();
            }

            _context.Repairs.Remove(repair);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RepairExists(int id)
        {
            return (_context.Repairs?.Any(e => e.ExpensesId == id)).GetValueOrDefault();
        }
    }
}
