using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BSCare.Data;
using BSCare.Models;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BSCare.Pages.report
{
    public class CreateModel2 : PageModel
    {
        private readonly BSCare.Data.BscareDbContext _context;

        public CreateModel2(BSCare.Data.BscareDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public List<SelectListItem> SelectList { get; set; }
        public List<SelectListItem> StatusSelectList { get; set; }
        public List<SelectListItem> HazardSelectList { get; set; }

        public List<int> stopNum = new List<int>();

        [BindProperty]
        public Report Report { get; set; }
        
        [BindProperty]
        public int Stop_code { get; set; }

        public async Task OnGet(int? id)
        {
            Stop_code = (int)id;
            SelectList = GlobalVar.SelectList;
            StatusSelectList = GlobalVar.StatusSelectList;
            HazardSelectList = GlobalVar.HazardSelectList;
            await getAllStops();
            //return Page();
            Page();
        }

        public async Task getAllStops()
        {
            List<Stop> stopList = new List<Stop>(await _context.Stops.ToListAsync());

            foreach (var stop in stopList)
            {
                if (stop.StopDesc != null)
                {
                    string s = stop.StopDesc;
                    int firstStringPosition = s.IndexOf("עיר:");
                    int secondStringPosition = s.IndexOf("רציף");

                    if (secondStringPosition - firstStringPosition - 6 > 0)
                    {
                        string city = s.Substring(firstStringPosition + 5, secondStringPosition - firstStringPosition - 6);

                        if (city == GlobalVar.city)
                        {
                            stopNum.Add(stop.StopCode);
                        }
                    }
                }
            }
        }


 


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Reports.Add(Report);
            await _context.SaveChangesAsync();

            return RedirectToPage("../Index2");
        }
    }
}
