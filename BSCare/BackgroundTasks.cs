
using BSCare.Data;

namespace BSCare
{
    public class BackgroundTasks : BackgroundService
    {
        /*    Uri url = new Uri("https://gtfs.mot.gov.il/gtfsfiles/israel-public-transportation.zip");

          await DownLoadFromUrl.DownloadFileAsync(@"C:\\Users\\grigo\\Desktop\\Afeka\\Final_Project_18_Transportation\\BSCare_app\\BSCare\\BSCare\\Resources\\1GB.zip", url, CancellationToken.None).ConfigureAwait(false);
      */
        Uri url = new Uri("https://gtfs.mot.gov.il/gtfsfiles/israel-public-transportation.zip");
        private readonly ILogger<BackgroundTasks> _logger;
        private readonly DateTime updateTime = new DateTime(2023, 6, 9, 00, 00, 00);

        public BackgroundTasks(ILogger<BackgroundTasks> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation(DateTime.Now.ToString());

                if(updateTime.Hour == DateTime.Now.Hour)
                {
                    if(updateTime.Minute == DateTime.Now.Minute)
                    {
                        _logger.LogInformation("Start Update");

                        //DownLoad & unzip
                        await DownLoadFromUrl.DownloadFileAsync(@"C:\\Users\\grigo\\Desktop\\Afeka\\Final_Project_18_Transportation\\BSCare_app\\BSCare\\BSCare\\Resources\\israel-public-transportation.zip", url, CancellationToken.None);

                        //Insert to DB
                        DownLoadFromUrl.OnPostUnzipGTFSAsync();

                        _logger.LogInformation("Update completed successfully");

                        //await Task.Delay(TimeSpan.FromMilliseconds(1000), stoppingToken);
                    }
                    else
                    {
                        _logger.LogInformation("Delay for 1 min");
                        await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                    }
                }
                else
                {
                    _logger.LogInformation("Delay for 1 hour");
                    await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
                }
            }
        }
    }
}
