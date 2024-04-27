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

namespace BSCare.Pages.repair
{
    public class EditModel : PageModel
    {
        private readonly BSCare.Data.BscareDbContext _context;

        public EditModel(BSCare.Data.BscareDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Repair Repair { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Repairs == null)
            {
                return NotFound();
            }

            var repair =  await _context.Repairs.FirstOrDefaultAsync(m => m.ExpensesId == id);
            if (repair == null)
            {
                return NotFound();
            }
            Repair = repair;
           ViewData["ReportId"] = new SelectList(_context.Reports, "ReportId", "ReportId");
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

            _context.Attach(Repair).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RepairExists(Repair.ExpensesId))
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

        private bool RepairExists(int id)
        {
          return (_context.Repairs?.Any(e => e.ExpensesId == id)).GetValueOrDefault();
        }
    }
}
