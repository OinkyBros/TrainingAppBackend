using Oinky.TrainingAppAPI.Services.Interfaces;

namespace Oinky.TrainingAppAPI.Services
{
    public class DataFetcherService : BackgroundService
    {
        private DataFetcher m_dataFetcher;

        public IConfiguration Configuration { get; private set; }

        public DataFetcherService(IConfiguration configuration, ILoggerFactory loggerFactory, ISummonerService summonerService, IMatchService matchService)
        {
            Configuration = configuration;
            m_dataFetcher = new DataFetcher(loggerFactory.CreateLogger<DataFetcher>(), Configuration, summonerService, matchService);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return m_dataFetcher.RunAsync(stoppingToken);
        }
    }
}