using Oinky.TrainingAppAPI.Models.Configuration;
using Oinky.TrainingAppAPI.Utils;

namespace Oinky.TrainingAppAPI.Services
{
    public class IconService
    {
        public bool IsValid { get => m_valid; }

        public IconService(ILogger<IconService> logger, IConfiguration configuration)
        {
            m_logger = logger;
            m_settings = configuration.GetSection("IconSettings").Get<IconSettings>();
            if (m_settings == null || m_settings.BasePath == null || m_settings.ChampionFolder == null || m_settings.ProfileFolder == null)
            {
                m_valid = false;
            }
            else
            {
                //Check if base path exists
                string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, m_settings.BasePath);
                if (!Directory.Exists(basePath))
                {
                    m_valid = false;
                    return;
                }
                //Check if Champion folder exists
                m_championPath = Path.Combine(basePath, m_settings.ChampionFolder);
                if (!Directory.Exists(m_championPath))
                {
                    m_valid = false;
                    return;
                }

                //Check if Profile folder exists
                m_profilePath = Path.Combine(basePath, m_settings.ProfileFolder);
                if (!Directory.Exists(m_profilePath))
                {
                    m_valid = false;
                    return;
                }


                m_valid = true;
            }
        }

        public FileInfo GetProfileIcon(int iconID)
        {
            if (!m_valid)
                return null;
            string path = Path.Combine(m_profilePath, iconID + ".png");
            if (!File.Exists(path))
                return null;
            return new FileInfo(path);
        }

        public FileInfo GetChampionIcon(string championName)
        {
            if (!m_valid)
                return null;
            string path = Path.Combine(m_championPath, championName + ".png");
            if (!File.Exists(path))
                return null;
            return new FileInfo(path);
        }

        private string m_championPath;
        private ILogger<IconService> m_logger;
        private string m_profilePath;
        private IconSettings m_settings;
        private bool m_valid;
    }
}