using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BSCare.Data;
using BSCare.Models;

namespace BSCare.Pages.stops
{
    public class EditModel : PageModel
    {
        private readonly BSCare.Data.BscareDbContext _context;

        public EditModel(BSCare.Data.BscareDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Stop Stop { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Stops == null)
            {
                return NotFound();
            }

            var stop =  await _context.Stops.FirstOrDefaultAsync(m => m.StopId == id);
            if (stop == null)
            {
                return NotFound();
            }
            Stop = stop;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Stop).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StopExists(Stop.StopId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool StopExists(int id)
        {
          return _context.Stops.Any(e => e.StopId == id);
        }
    }
}
