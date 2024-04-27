using BSCare.Data;
using BSCare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace BSCare.Pages
{
    public class Index2Model : PageModel
    {
        private readonly BSCare.Data.BscareDbContext _context;
        public Index2Model(BSCare.Data.BscareDbContext context)
        {
            _context = context;
        }
        
        public int stopCount { get; set; }

        [BindProperty]
        public int myId { get; set; }

        [BindProperty]
        public List<Stop> stopList { get; set; }

        [BindProperty]
        public List<Stop> cities { get; set; }

        [BindProperty]
        public string Frontcity { get; set; }
        
        [BindProperty]
        public int OpenReportsWeb { get; set; }

        [BindProperty]
        public int OpenReportCell { get; set; }

        [BindProperty]
        public int forwardedReports { get; set; }
        [BindProperty]
        public int closedReports { get; set; }
 
        [BindProperty]
        public int numOfrepair { get; set; }

        public IList<Stop> Stop { get; set; } = default!;

        public async Task OnGet()
        {
            stopList = new List<Stop>(await _context.Stops.ToListAsync());
            List<Stop> cities = new List<Stop>();

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
                            var repairs = await _context.Repairs.Where(m => m.StopCode == stop.StopCode).ToListAsync();
                            numOfrepair += repairs.Count;

                            var reports = await _context.Reports.Where(m => m.StopId == stop.StopCode).ToListAsync();
                            foreach (var report in reports)
                            {
                                if (report.Status == 0)
                                {
                                    if (report.ReportSource == 0)
                                        OpenReportsWeb++;
                                    else if (report.ReportSource == 1)
                                        OpenReportCell++;
                                }
                                else if (report.Status == 1)
                                    forwardedReports++;
                                else if (report.Status == 2)
                                    closedReports++;
                            }
                        }
                    }
                }
            }
            stopCount = cities.Count;
        }

        public async Task OnPostSelectCityAsync()
        {
            GlobalVar.city = Frontcity;
            await OnGet();

        }

        public async Task OnPostBackAsync()
        {
            Frontcity = GlobalVar.city;
            await OnGet();

        }
    }
}

        
  
