using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BSCare.Data;
using BSCare.Models;
using Microsoft.EntityFrameworkCore;

namespace BSCare.Pages.repair
{
    public class CreateModel : PageModel
    {
        private readonly BSCare.Data.BscareDbContext _context;

        public CreateModel(BSCare.Data.BscareDbContext context)
        {
            _context = context;
        }

        public int reportId { get; set; }
        public int Stop_code { get; set; }
        public IActionResult OnGet(int? id, int? code)
        {
            //ViewData["ReportId"] = new SelectList(_context.Reports, "ReportId", "ReportId");
            reportId = (int)id;
            Stop_code = (int)code;
            return Page();
        }

        [BindProperty]
        public Repair Repair { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          /*if (!ModelState.IsValid || _context.Repairs == null || Repair == null)
            {
                return Page();
            }*/

            _context.Repairs.Add(Repair);
            Report temp = await _context.Reports.FindAsync(Repair.ReportId);
            temp.Status = 2;
            temp.CloseDate= DateTime.Now;
            _context.Entry(temp).State = EntityState.Modified;
            await _context.SaveChangesAsync();


            return RedirectToPage("../Index2");
        }
    }
}
