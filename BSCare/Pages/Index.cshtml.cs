using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using BSCare.Data;
using Microsoft.EntityFrameworkCore;
using BSCare.Models;
using System.Drawing;
using System.Net;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Security.Policy;
using System.Diagnostics;
using System.IO.Compression;

namespace BSCare.Pages
{
    public class IndexModel : PageModel
    {
        //
        private readonly BSCare.Data.BscareDbContext _context;
        //

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, BSCare.Data.BscareDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        public List<Stop> stopList { get; set; }

        [BindProperty]
        public string Frontcity { get; set; }

        [BindProperty]
        public List<string> cities { get; set; }


        public async Task OnGet()
        {
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
            }
        }

        public async Task OnPostDownLoadFileAsync()
        {
            /*await DownLoadFromUrl.DownloadFile("https://gtfs.mot.gov.il/gtfsfiles/israel-public-transportation.zip",
                                "C:\\Users\\grigo\\Desktop\\Afeka\\Final_Project_18_Transportation\\BSCare_app\\BSCare\\BSCare\\Resources",
                                "new_stops");*/


        /*    Uri url = new Uri("https://gtfs.mot.gov.il/gtfsfiles/israel-public-transportation.zip");

            await DownLoadFromUrl.DownloadFileAsync(@"C:\\Users\\grigo\\Desktop\\Afeka\\Final_Project_18_Transportation\\BSCare_app\\BSCare\\BSCare\\Resources\\1GB.zip", url, CancellationToken.None).ConfigureAwait(false);
        */
        
        }
       /* public void OnPostUnzipGTFSAsync()
        {
            //string zipPath = @".\result.zip";
            //string extractPath = @".\extract";
            string zipPath = "C:\\Users\\grigo\\Desktop\\Afeka\\Final_Project_18_Transportation\\BSCare_app\\BSCare\\BSCare\\Resources\\israel-public-transportation.zip";
            string extractPath = "C:\\Users\\grigo\\Desktop\\Afeka\\Final_Project_18_Transportation\\BSCare_app\\BSCare\\BSCare\\Resources\\Unziped";
            ZipFile.ExtractToDirectory(zipPath, extractPath);
        }*/

        /*public async Task OnPostInsertToDBAsync()
        {
            //delete all records in dbo.stops
            await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE [dbo.stops]");

            using (var reader = new StreamReader("C:\\Users\\grigo\\Desktop\\Afeka\\Final_Project_18_Transportation\\BSCare_app\\BSCare\\BSCare\\Resources\\stops.txt"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();

                List<int> stop_c = new List<int>();

                while (csv.Read())
                {
                    var record = csv.GetRecord<Stop>();
                    if(!stop_c.Contains(record.StopCode))
                    {
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

                        stop_c.Add(stop.StopCode);

                        _context.Stops.Add(stop);
                    }
                }
                await _context.SaveChangesAsync();
            }
        }*/
    }
}