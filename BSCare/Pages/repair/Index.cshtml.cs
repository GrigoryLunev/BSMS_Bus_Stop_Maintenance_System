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
    public class IndexModel : PageModel
    {
        private readonly BSCare.Data.BscareDbContext _context;

        public IndexModel(BSCare.Data.BscareDbContext context)
        {
            _context = context;
        }

        public IList<Repair> Repair { get;set; } = default!;


        public async Task OnGetAsync()
        {
            if (_context.Repairs != null)
            {
                Repair = await _context.Repairs
                .Include(r => r.Report).ToListAsync();
            }
        }

        public async Task OnPostQ5Index2Async()
        {
            List<Stop> stopList = new List<Stop>(await _context.Stops.ToListAsync());
            List<Stop> cities = new List<Stop>();
            List<Repair> allRepair = new List<Repair>();


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
                            cities.Add(stop);
                            var temp = await _context.Repairs.Where(m => m.StopCode == stop.StopCode).ToListAsync();

                            //var reports = await _context.Reports.Where(m => m.StopId == stop.StopCode).ToListAsync();
                            foreach (var r in temp)
                            {
                                allRepair.Add(r);
                            }
                        }
                    }
                }
            }
            Repair = allRepair;
        }
    }
}
