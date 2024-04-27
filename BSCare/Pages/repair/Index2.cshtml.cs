using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BSCare.Data;
using BSCare.Models;
using FusionCharts.DataEngine;
using FusionCharts.Visualization;
using System.Data;

namespace BSCare.Pages.repair
{
    public class IndexModel2 : PageModel
    {
        private readonly BSCare.Data.BscareDbContext _context;

        public IndexModel2(BSCare.Data.BscareDbContext context)
        {
            _context = context;
        }

        public IList<Repair> Repair { get;set; } = default!;

        public List<Report> Reports { get; set; } = default!;

        public List<int> years { get; set; }

        public int totalCost { get; set; }

        public string ChartJson { get; internal set; }
        public string ChartJson2 { get; internal set; }

        [BindProperty]
        public int y { get; set; }


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
            //Chart1
            // create data table to store data
            DataTable ChartData = new DataTable();
            // Add columns to data table
            ChartData.Columns.Add("שנה", typeof(System.String));
            ChartData.Columns.Add("עלות תחזוקה", typeof(System.Double));
            //Chart1

            years = new List<int>();

            List<Stop> stopList = new List<Stop>(await _context.Stops.ToListAsync());
            List<Stop> cities = new List<Stop>();
            List<Repair> allRepair = new List<Repair>();
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

                            var temp = await _context.Repairs.Where(m => m.StopCode == stop.StopCode).ToListAsync();
                            foreach (var r in temp)
                            {
                                allRepair.Add(r);
                            }

                            var reports = await _context.Reports.Where(m => m.StopId == stop.StopCode).ToListAsync();
                            foreach (var report in reports)
                            {
                                if (report.Status == 2 && report.CloseDate != null)
                                {
                                    allReports.Add(report);
                                    int year = report.CloseDate.Value.Year;
                                    if (!years.Contains(year))
                                    {
                                        years.Add(year);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            years.Sort();
            //Repair = allRepair;
            Repair = new List<Repair>();

            //Chart Dictionary1
            IDictionary<int, double> yearPrice = new Dictionary<int, double>();
            foreach (var report in allReports)
            {
                foreach (var repair in allRepair)
                {
                    if (report.ReportId == repair.ReportId)
                    {
                        if (yearPrice.ContainsKey(report.CloseDate.Value.Year))
                        {
                            yearPrice[report.CloseDate.Value.Year] += repair.ActionPrice;
                        }
                        else
                            yearPrice.Add(report.CloseDate.Value.Year, repair.ActionPrice);
                    }
                }
            }
            foreach (KeyValuePair<int, double> kvp in yearPrice)
                ChartData.Rows.Add(kvp.Key.ToString(), kvp.Value);

            // Create static source with this data table
            StaticSource source = new StaticSource(ChartData);
            // Create instance of DataModel class
            DataModel model = new DataModel();
            // Add DataSource to the DataModel
            model.DataSources.Add(source);
            // Instantiate Column Chart
            Charts.ColumnChart column = new Charts.ColumnChart("first_chart");
            // Set Chart's width and height
            //column.Width.Pixel(700);
            //column.Height.Pixel(400);
            // Set DataModel instance as the data source of the chart
            column.Data.Source = model;
            // Set Chart Title
            column.Caption.Text = "עלות הוצאות תיקונים ותחזוקה";
            // Set chart sub title
            column.SubCaption.Text = "כל השנים";
            // hide chart Legend
            column.Legend.Show = false;
            // set XAxis Text
            column.XAxis.Text = "שנים";
            // Set YAxis title
            column.YAxis.Text = "עלות בשקלים";
            // set chart theme
            column.ThemeName = FusionChartsTheme.ThemeName.FUSION;
            // set chart rendering json
            //ChartJson = column.Render();
            //End Chart
            //
            //

        }

        public async Task OnPostQ6Index2Async()
        {
            //Chart1
            // create data table to store data
            DataTable ChartData = new DataTable();
            // Add columns to data table
            ChartData.Columns.Add("שנה", typeof(System.String));
            ChartData.Columns.Add("עלות תחזוקה", typeof(System.Double));
            //Chart1

            //Chart2
            // create data table to store data
            DataTable ChartData2 = new DataTable();
            // Add columns to data table
            ChartData2.Columns.Add("שנה", typeof(System.String));
            ChartData2.Columns.Add("עלות תחזוקה", typeof(System.Double));
            //Chart2


            years = new List<int>();

            List<Stop> stopList = new List<Stop>(await _context.Stops.ToListAsync());
            List<Stop> cities = new List<Stop>();
            List<Repair> allRepair = new List<Repair>();
            List<Report> allReports = new List<Report>();
            List<Report> allReportsAllYears = new List<Report>();

            List<Repair> yearRepair = new List<Repair>();


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
                            foreach (var r in temp)
                            {
                                allRepair.Add(r);
                            }

                            var reports = await _context.Reports.Where(m => m.StopId == stop.StopCode).ToListAsync();
                            foreach (var report in reports)
                            {
                                if (report.Status == 2 && report.CloseDate != null)
                                {
                                    allReportsAllYears.Add(report);

                                    int year = report.CloseDate.Value.Year;
                                    if(year == y)
                                    {
                                        allReports.Add(report);
                                    }
                                    
                           
                                    if (!years.Contains(year))
                                    {
                                        years.Add(year);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            years.Sort();
            //Chart Dictionary1
            IDictionary<int, double> yearPrice = new Dictionary<int, double>();

            //Chart Dictionary2
            IDictionary<int, int> hazardType = new Dictionary<int, int>();

            foreach (var report in allReports)
            {
                foreach (var repair in allRepair)
                {
                    if (report.ReportId == repair.ReportId)
                    {
                        yearRepair.Add(repair);
                        totalCost += repair.ActionPrice;

                        //Chart2
                        if (hazardType.ContainsKey(report.HazardType))
                        {
                            hazardType[report.HazardType] += 1;
                        }
                        else
                            hazardType.Add(report.HazardType, 1);
                    }
                }
            }
            foreach (var report2 in allReportsAllYears)
            {
                foreach (var repair in allRepair)
                {
                    if (report2.ReportId == repair.ReportId)
                    {
                        //Chart1
                        if (yearPrice.ContainsKey(report2.CloseDate.Value.Year))
                        {
                            yearPrice[report2.CloseDate.Value.Year] += repair.ActionPrice;
                        }
                        else
                            yearPrice.Add(report2.CloseDate.Value.Year, repair.ActionPrice);
                    }
                }
            }
            Repair = yearRepair;
            //Chart1
            foreach (KeyValuePair<int, double> kvp in yearPrice)
                ChartData.Rows.Add(kvp.Key.ToString(), kvp.Value);

            //Chart2
            foreach (KeyValuePair<int, int> kvp2 in hazardType)
            {
                if(kvp2.Key == 0)
                {
                    ChartData2.Rows.Add("תקלה מכנית", kvp2.Value);
                }
                else if(kvp2.Key == 1)
                {
                    ChartData2.Rows.Add("תקלת תאורה", kvp2.Value);
                }
                else if(kvp2.Key == 2)
                {
                    ChartData2.Rows.Add("תקלת ניקיון", kvp2.Value);
                }
                else if(kvp2.Key == 3)
                {
                    ChartData2.Rows.Add("תקלה בלוח הדיגיטלי", kvp2.Value);
                }
                else
                    ChartData2.Rows.Add("אחר", kvp2.Value);
            }

            //Chart2
            // Create static source with this data table
            StaticSource source2 = new StaticSource(ChartData2);
            // Create instance of DataModel class
            DataModel model2 = new DataModel();
            // Add DataSource to the DataModel
            model2.DataSources.Add(source2);
            // Instantiate Column Chart
            Charts.PieChart column2 = new Charts.PieChart("second_chart");
            // Set Chart's width and height
            //column2.Width.Pixel(700);
            //column2.Height.Pixel(400);
            // Set DataModel instance as the data source of the chart
            column2.Data.Source = model2;
            // Set Chart Title
            column2.Caption.Text = "התפלגות סוגי תקלות";
            // Set chart sub title
            column2.SubCaption.Text = y.ToString();
            // hide chart Legend
            column2.Legend.Show = false;
            // set XAxis Text
            //column2.XAxis.Text = "שנים";
            // Set YAxis title
            //column2.YAxis.Text = "עלות בשקלים";
            // set chart theme
            column2.ThemeName = FusionChartsTheme.ThemeName.FUSION;
            // set chart rendering json
            ChartJson2 = column2.Render();



            //Chart1
            // Create static source with this data table
            StaticSource source = new StaticSource(ChartData);
            // Create instance of DataModel class
            DataModel model = new DataModel();
            // Add DataSource to the DataModel
            model.DataSources.Add(source);
            // Instantiate Column Chart
            Charts.ColumnChart column = new Charts.ColumnChart("first_chart");
            // Set Chart's width and height
            //column.Width.Pixel(700);
            //column.Height.Pixel(400);
            // Set DataModel instance as the data source of the chart
            column.Data.Source = model;
            // Set Chart Title
            column.Caption.Text = "עלות הוצאות תיקונים ותחזוקה";
            // Set chart sub title
            column.SubCaption.Text = "כל השנים";
            // hide chart Legend
            column.Legend.Show = false;
            // set XAxis Text
            column.XAxis.Text = "שנים";
            // Set YAxis title
            column.YAxis.Text = "עלות בשקלים";
            // set chart theme
            column.ThemeName = FusionChartsTheme.ThemeName.FUSION;
            // set chart rendering json
            ChartJson = column.Render();
        }
    }
}
