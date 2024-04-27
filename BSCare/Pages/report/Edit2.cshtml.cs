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

namespace BSCare.Pages.report
{
    public class EditModel2 : PageModel
    {
        private readonly BSCare.Data.BscareDbContext _context;

        public EditModel2(BSCare.Data.BscareDbContext context)
        {
            _context = context;
        }
        public List<SelectListItem> EditStatusSelectList { get; set; }
        public List<SelectListItem> EditInitiatorList { get; set; }
        
        public List<SelectListItem> HazardSelectList { get; set; }


        [BindProperty]
        public Report Report { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            EditStatusSelectList = GlobalVar.EditStatusSelectList;
            EditInitiatorList = GlobalVar.EditInitiatorList;
            HazardSelectList = GlobalVar.HazardSelectList;

            if (id == null || _context.Reports == null)
            {
                return NotFound();
            }

            var report =  await _context.Reports.FirstOrDefaultAsync(m => m.ReportId == id);
            if (report == null)
            {
                return NotFound();
            }
            Report = report;
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

            _context.Attach(Report).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportExists(Report.ReportId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("../Index2");
        }

        private bool ReportExists(int id)
        {
          return _context.Reports.Any(e => e.ReportId == id);
        }
    }
}
