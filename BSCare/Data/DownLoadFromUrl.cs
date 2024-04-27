using BSCare.Models;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.IO.Compression;

namespace BSCare.Data
{
    public class DownLoadFromUrl
    {
        private readonly BSCare.Data.BscareDbContext _context;

        public DownLoadFromUrl(BSCare.Data.BscareDbContext context)
        {
            _context = context;
        }

        private const int HttpBufferSize = 81920;

        public static async Task DownloadFileAsync(string filePath, Uri fileEndpoint,
    CancellationToken token)
        {
            using var client = new HttpClient();

            using var response = await client.GetAsync(fileEndpoint, HttpCompletionOption.ResponseHeadersRead, token).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            await using var contentStream = await response.Content.ReadAsStreamAsync(token).ConfigureAwait(false);
            await using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            await contentStream.CopyToAsync(stream, HttpBufferSize, token).ConfigureAwait(false);
            OnPostUnzipGTFSAsync();
        }

        public static void OnPostUnzipGTFSAsync()
        {
            string zipPath = "C:\\Users\\grigo\\Desktop\\Afeka\\Final_Project_18_Transportation\\BSCare_app\\BSCare\\BSCare\\Resources\\israel-public-transportation.zip";
            string extractPath = "C:\\Users\\grigo\\Desktop\\Afeka\\Final_Project_18_Transportation\\BSCare_app\\BSCare\\BSCare\\Resources";
            ZipFile.ExtractToDirectory(zipPath, extractPath);
        }

        public async Task OnPostInsertToDBAsync()
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
                    if (!stop_c.Contains(record.StopCode))
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

            /*if (_context.Stops != null)
            {
                Stop = await _context.Stops.ToListAsync();
            }*/
        }
    }
}
