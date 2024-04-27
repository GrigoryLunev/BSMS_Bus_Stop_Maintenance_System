using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BSCare.Data;
using BSCare.Models;

namespace BSCare.Pages.repair
{
    public class DeleteModel : PageModel
    {
        private readonly BSCare.Data.BscareDbContext _context;

        public DeleteModel(BSCare.Data.BscareDbContext context)
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

            var repair = await _context.Repairs.FirstOrDefaultAsync(m => m.ExpensesId == id);

            if (repair == null)
            {
                return NotFound();
            }
            else 
            {
                Repair = repair;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Repairs == null)
            {
                return NotFound();
            }
            var repair = await _context.Repairs.FindAsync(id);

            if (repair != null)
            {
                Repair = repair;
                _context.Repairs.Remove(Repair);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
