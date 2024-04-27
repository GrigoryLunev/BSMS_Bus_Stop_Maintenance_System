using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BSCare.Data;
using BSCare.Models;

namespace BSCare.Pages.report
{
    public class DetailsModel : PageModel
    {
        private readonly BSCare.Data.BscareDbContext _context;

        public DetailsModel(BSCare.Data.BscareDbContext context)
        {
            _context = context;
        }

      public Report Report { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Reports == null)
            {
                return NotFound();
            }

            var report = await _context.Reports.FirstOrDefaultAsync(m => m.ReportId == id);
            if (report == null)
            {
                return NotFound();
            }
            else 
            {
                Report = report;
            }
            return Page();
        }
    }
}
