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
    public class DetailsModel : PageModel
    {
        private readonly BSCare.Data.BscareDbContext _context;

        public DetailsModel(BSCare.Data.BscareDbContext context)
        {
            _context = context;
        }

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
    }
}
