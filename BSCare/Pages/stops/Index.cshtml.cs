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

namespace BSCare.Pages.stops
{
    public class IndexModel : PageModel
    {
        private readonly BSCare.Data.BscareDbContext _context;

        public IndexModel(BSCare.Data.BscareDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public List<Stop> stopList { get; set; }

        [BindProperty]
        public List<string> cities { get; set; }

        [BindProperty]
        public string Frontcity { get; set; }

        [BindProperty]
        public int find { get; set; }

        public IList<Stop> Stop { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Frontcity = GlobalVar.city;
            cities = new List<string>();

            if (_context.Stops != null)
            {
                //Stop = await _context.Stops.ToListAsync();
                stopList = new List<Stop>(await _context.Stops.ToListAsync());


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

                            if (!cities.Contains(city))
                            {
                                cities.Add(city);
                            }
                        }
                    }
                }
                Stop = new List<Stop>();
                Stop.Add(stopList[0]);
            }
        }

        public async Task OnPostDownLoadFileAsync()
        {
            using (var reader = new StreamReader("C:\\Users\\grigo\\Desktop\\Afeka\\Final_Project_18_Transportation\\BSCare_app\\BSCare\\BSCare\\Resources\\stops.txt"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var record = csv.GetRecord<Stop>();
                    // Do something with the record.
                    var stop = new Stop();
                    stop.StopId = record.StopId;
                    stop.StopCode = record.StopCode;
                    stop.StopName = record.StopName;
                    stop.StopDesc = record.StopDesc;
                    stop.StopLat = record.StopLat;
                    stop.StopLon = record.StopLon;
                    stop.LocationType = record.LocationType;
                    stop.ParentStation = record.ParentStation;
                    stop.ZoneId = record.ZoneId;

                    _context.Stops.Add(stop);
                    //await _context.SaveChangesAsync();
                }
                await _context.SaveChangesAsync();
            }

            /*if (_context.Stops != null)
            {
                Stop = await _context.Stops.ToListAsync();
            }*/
        }

        public async Task OnPostSelectCityAsync()
        {
            //
            GlobalVar.city = Frontcity;
            //
            stopList = new List<Stop>(await _context.Stops.ToListAsync());
            List<Stop> temp = new List<Stop>();
            cities = new List<string>();
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

                        if (string.Compare(city, Frontcity) == 0)
                        {
                            temp.Add(stop);
                        }

                        if (!cities.Contains(city))
                        {
                            cities.Add(city);
                        }
                    }
                }
            }
            Stop = new List<Stop>(temp);
        }


        public async Task OnPostQ1Index2Async()
        {
            Frontcity = GlobalVar.city;
            stopList = new List<Stop>(await _context.Stops.ToListAsync());
            List<Stop> temp = new List<Stop>();
            cities = new List<string>();
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
                            temp.Add(stop);
                        }
                    }
                }
            }
            Stop = new List<Stop>(temp);
        }

        public async Task OnPostFindCityAsync()
        {
            
            Stop = await _context.Stops.Where(m=>m.StopCode == find).ToListAsync();
        }

    }
}
