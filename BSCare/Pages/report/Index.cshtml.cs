using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BSCare.Data;
using BSCare.Models;
using CsvHelper;
using System.Globalization;

namespace BSCare.Pages.report
{
    public class IndexModel : PageModel
    {
        private readonly BSCare.Data.BscareDbContext _context;
        public string city;

        [BindProperty]
        public string Frontcity { get; set; }

        public IndexModel(BSCare.Data.BscareDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public int source { get; set; }

        public IList<Report> Report { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Reports != null)
            {
                Report = await _context.Reports.ToListAsync();
            }
        }

        public async Task OnPostQ2Index2Async()
        {
            Frontcity = GlobalVar.city;
            List<Stop>stopList = new List<Stop>(await _context.Stops.ToListAsync());
            List<Stop> cities = new List<Stop>();
            List<Report> allReports = new List<Report>();


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
                            var reports = await _context.Reports.Where(m => m.StopId == stop.StopCode).ToListAsync();
                            foreach (var report in reports)
                            {
                                if(source == report.ReportSource && report.Status == 0)
                                {
                                    allReports.Add(report);
                                }
                            }
                        }
                    }
                }
            }
            Report = allReports;
        }

        public async Task OnPostQ3Index2Async()
        {
            Frontcity = GlobalVar.city;
            List<Stop> stopList = new List<Stop>(await _context.Stops.ToListAsync());
            List<Stop> cities = new List<Stop>();
            List<Report> allReports = new List<Report>();


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
                            var reports = await _context.Reports.Where(m => m.StopId == stop.StopCode).ToListAsync();
                            foreach (var report in reports)
                            {
                                if (report.Status == 1)
                                {
                                    allReports.Add(report);
                                }
                            }
                        }
                    }
                }
            }
            Report = allReports;
        }

        public async Task OnPostQ4Index2Async()
        {
            Frontcity = GlobalVar.city;
            List<Stop> stopList = new List<Stop>(await _context.Stops.ToListAsync());
            List<Stop> cities = new List<Stop>();
            List<Report> allReports = new List<Report>();


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
                            var reports = await _context.Reports.Where(m => m.StopId == stop.StopCode).ToListAsync();
                            foreach (var report in reports)
                            {
                                if (report.Status == 2)
                                {
                                    allReports.Add(report);
                                }
                            }
                        }
                    }
                }
            }
            Report = allReports;
        }

    }
}
