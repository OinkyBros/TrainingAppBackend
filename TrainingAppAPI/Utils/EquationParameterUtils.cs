using Oinky.TrainingAppAPI.Models.DB;
using Oinky.TrainingAppAPI.Models.Extensions;

namespace Oinky.TrainingAppAPI.Utils
{
    public enum Parameter
    {
        INVALID = -1,
        VISIONSCORE,
        CS,
        DURATION
    }

    public enum ParameterCategory
    {
        INVALID = -1,
        MATCH,
        TEAM,
        PARTICIPANT
    }

    public class EquationParameterUtils
    {
        static EquationParameterUtils()
        {
            m_parameters = new Dictionary<ParameterCategory, List<Parameter>>();
            m_parameters[ParameterCategory.MATCH] = new List<Parameter>()
            {
                Parameter.DURATION
            };

            m_parameters[ParameterCategory.TEAM] = new List<Parameter>()
            {
                //Parameter.CS,
                //Parameter.VISIONSCORE
            };

            m_parameters[ParameterCategory.PARTICIPANT] = new List<Parameter>()
            {
                Parameter.CS,
                Parameter.VISIONSCORE
            };
        }

        public static bool TryGetParameterValue(ParameterCategory category, Parameter parameter, MatchDB matchD, string puuid, out double result)
        {
            result = double.MinValue;
            try
            {
                switch (category)
                {
                    case ParameterCategory.MATCH:
                        if (!GetMatchParameter(parameter, matchD, out result))
                            return false;
                        return true;

                    case ParameterCategory.TEAM:
                        if (!GetTeamParameter(parameter, matchD, puuid, out result))
                            return false;
                        return true;

                    case ParameterCategory.PARTICIPANT:
                        if (!GetParticipantParameter(parameter, matchD, puuid, out result))
                            return false;
                        return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return false;
        }

        public static bool ValidateParameter(string parameterString, out ParameterCategory category, out Parameter parameter)
        {
            category = ParameterCategory.INVALID;
            parameter = Parameter.INVALID;
            parameterString = parameterString.Replace(STARTING_CHAR, string.Empty);
            parameterString = parameterString.Replace(ENDING_CHAR, string.Empty);
            string[] splittedParameter = parameterString.Split(SPLITTING_CHAR);
            if (splittedParameter.Length != 2)
                return false;
            if (!Enum.TryParse(splittedParameter[0], out category))
                return false;
            if (!Enum.TryParse(splittedParameter[1], out parameter))
                return false;
            return m_parameters.ContainsKey(category) && m_parameters[category].Contains(parameter);
        }

        private static bool GetMatchParameter(Parameter parameter, MatchDB match, out double result)
        {
            result = double.MinValue;
            bool flag = true;
            try
            {
                switch (parameter)
                {
                    case Parameter.DURATION:
                        result = match.GameDuration;
                        break;

                    default:
                        flag = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return flag;
        }

        private static bool GetParticipantParameter(Parameter parameter, MatchDB match, string puuid, out double result)
        {
            result = double.MinValue;
            bool flag = true;
            ParticipantDB participant = match.GetParticipantByPUUID(puuid);
            if (participant == null)
                return false;
            try
            {
                switch (parameter)
                {
                    case Parameter.CS:
                        result = participant.TotalMinionsKilled + participant.NeutralMinionsKilled;
                        break;

                    case Parameter.VISIONSCORE:
                        result = participant.VisionScore;
                        break;

                    default:
                        flag = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return flag;
        }

        private static bool GetTeamParameter(Parameter parameter, MatchDB match, string puuid, out double result)
        {
            result = double.MinValue;
            bool flag = true;
            TeamDB team = match.GetTeamByPUUID(puuid);
            if (team == null)
                return false;
            try
            {
                switch (parameter)
                {
                    default:
                        flag = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return flag;
        }

        public static readonly string ENDING_CHAR = "}";
        public static readonly string SPLITTING_CHAR = ":";
        public static readonly string STARTING_CHAR = "{";
        private static Dictionary<ParameterCategory, List<Parameter>> m_parameters = null;
    }
}