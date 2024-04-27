using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BSCare.Data;
using BSCare.Models;

namespace BSCare.Pages.stops
{
    public class CreateModel : PageModel
    {
        private readonly BSCare.Data.BscareDbContext _context;

        public CreateModel(BSCare.Data.BscareDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Stop Stop { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Stops.Add(Stop);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
            //return RedirectToPage("./stops?handler=Q1Index2");
        }
    }
}
