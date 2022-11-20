using Oinky.TrainingAppAPI.Models.DB;
using Oinky.TrainingAppAPI.Models.Extensions;
using System.Globalization;
using System.Reflection;

namespace Oinky.TrainingAppAPI.Utils
{
    public enum ParameterCategory
    {
        INVALID = -1,
        MATCH,
        TEAM,
        PARTICIPANT
    }

    public class EquationParameterUtils
    {

        public static Dictionary<string, List<string>> Parameters { get; private set; }

        private static bool IsNumericType(Type type)
        {
            return m_numericTypes.Contains(type);
        }

        private static Dictionary<string, string> GetTuples(Type t)
        {
            PropertyInfo[] property = t.GetProperties();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            foreach (PropertyInfo p in property)
                if (IsNumericType(p.PropertyType))
                    parameters.Add(p.Name.ToUpper(), p.Name);
            return parameters;
        }

        static EquationParameterUtils()
        {
            m_parameters = new Dictionary<ParameterCategory, Dictionary<string, string>>();
            //Match
            m_parameters.Add(ParameterCategory.MATCH, GetTuples(typeof(MatchDB)));
            //Team
            m_parameters.Add(ParameterCategory.TEAM, GetTuples(typeof(TeamDB)));
            //Participant
            m_parameters.Add(ParameterCategory.PARTICIPANT, GetTuples(typeof(ParticipantDB)));

            //Get public parameters
            Parameters = new Dictionary<string, List<string>>();
            Parameters.Add(ParameterCategory.MATCH.ToString(), m_parameters[ParameterCategory.MATCH].Select(s => s.Key).ToList());
            Parameters.Add(ParameterCategory.TEAM.ToString(), m_parameters[ParameterCategory.TEAM].Select(s => s.Key).ToList());
            Parameters.Add(ParameterCategory.PARTICIPANT.ToString(), m_parameters[ParameterCategory.PARTICIPANT].Select(s => s.Key).ToList());
        }

        public static bool TryGetParameterValue(ParameterCategory category, string parameter, MatchDB matchDB, string puuid, out double result)
        {
            result = double.MinValue;
            try
            {
                if (!m_parameters.TryGetValue(category, out Dictionary<string, string> parameters))
                    return false;
                object obj = null;
                switch (category)
                {
                    case ParameterCategory.MATCH:
                        obj = matchDB;
                        break;
                    case ParameterCategory.TEAM:
                        obj = matchDB.GetTeamByPUUID(puuid);
                        break;
                    case ParameterCategory.PARTICIPANT:
                        obj = matchDB.GetParticipantByPUUID(puuid);
                        break;
                    default:
                        return false;
                }
                if (obj == null)
                    return false;
                if (TryToParseProperty(obj, parameter, out result))
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private static bool TryToParseProperty(object obj, string property, out double result)
        {
            result = Double.MinValue;
            try
            {
                var propertyVal = obj.GetType().GetProperty(property).GetValue(obj);
                if (propertyVal == null)
                    return false;
                if (propertyVal is IConvertible)
                {
                    result = ((IConvertible)propertyVal).ToDouble(CultureInfo.InvariantCulture);
                }
                else
                    return false;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public static bool ValidateParameter(string parameterString, out ParameterCategory category, out string parameter)
        {
            category = ParameterCategory.INVALID;
            parameter = null;
            parameterString = parameterString.Replace(STARTING_CHAR, string.Empty);
            parameterString = parameterString.Replace(ENDING_CHAR, string.Empty);
            string[] splittedParameter = parameterString.Split(SPLITTING_CHAR);
            if (splittedParameter.Length != 2)
                return false;
            if (!Enum.TryParse(splittedParameter[0], out category))
                return false;
            if (!m_parameters.ContainsKey(category))
                return false;
            Dictionary<string, string> parameters = m_parameters[category];
            return parameters.TryGetValue(splittedParameter[1], out parameter);
        }

        public static readonly string ENDING_CHAR = "}";
        public static readonly string SPLITTING_CHAR = ":";
        public static readonly string STARTING_CHAR = "{";

        private static HashSet<Type> m_numericTypes = new HashSet<Type>
        {
            typeof(decimal), typeof(long),typeof(int), typeof(double)
        };

        private static Dictionary<ParameterCategory, Dictionary<string, string>> m_parameters = null;
    }
}