using Oinky.TrainingAppAPI.Models.Configuration;

namespace Oinky.TrainingAppAPI.Utils
{
    public class APIUtils
    {
        public static APISettings APISettings { get; set; }

        public static List<string> Oinkies
        {
            get
            {
                if (APISettings == null || APISettings.Oinkies == null)
                    return new List<string>();
                return APISettings.Oinkies;
            }
        }

        public static long StartingTimestamp
        {
            get
            {
                if (APISettings == null)
                    return 1659304800;
                return APISettings.StartingTimestamp;
            }
        }

        public static readonly string CONTENT_ROOT_PATH_PARAM = "ENV_ROOT_PATH";
    }
}