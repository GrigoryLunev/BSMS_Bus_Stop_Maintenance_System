using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BSCare.Data;
using BSCare.Models;

namespace BSCare.Pages.stops
{
    public class DeleteModel : PageModel
    {
        private readonly BSCare.Data.BscareDbContext _context;

        public DeleteModel(BSCare.Data.BscareDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Stop Stop { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Stops == null)
            {
                return NotFound();
            }

            var stop = await _context.Stops.FirstOrDefaultAsync(m => m.StopId == id);

            if (stop == null)
            {
                return NotFound();
            }
            else 
            {
                Stop = stop;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Stops == null)
            {
                return NotFound();
            }
            var stop = await _context.Stops.FindAsync(id);

            if (stop != null)
            {
                Stop = stop;
                _context.Stops.Remove(Stop);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
