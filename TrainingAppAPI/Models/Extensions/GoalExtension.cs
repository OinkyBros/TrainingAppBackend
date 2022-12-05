using Oinky.TrainingAppAPI.Models.DB;
using Oinky.TrainingAppAPI.Models.Request;
using Oinky.TrainingAppAPI.Models.Result;
using Oinky.TrainingAppAPI.Utils;

namespace Oinky.TrainingAppAPI.Models.Extensions
{
    public static class GoalExtension
    {

        public static ExtendedGoalDTO ConvertToExtended(this GoalDB goalDB)
        {
            return new ExtendedGoalDTO()
            {
                BotGoal = goalDB.BotGoal,
                GoalID = goalDB.GoalID,
                DisplayName = goalDB.DisplayName,
                JungleGoal = goalDB.JungleGoal,
                MidGoal = goalDB.MidGoal,
                SuppGoal = goalDB.SuppGoal,
                TopGoal = goalDB.TopGoal
            };
        }

        public static GoalDB ConvertToDB(this AddGoalRequest request)
        {
            MathEquationParser parser = new MathEquationParser();

            if (request.DisplayName == null)
                return null;

            //Check all goals if they are parseable
            //TOP
            bool flag = parser.ValidateExpression(request.TopGoal);
            if (!flag)
                return null;
            //JUNGLE
            flag = parser.ValidateExpression(request.JungleGoal);
            if (!flag)
                return null;
            //MID
            flag = parser.ValidateExpression(request.MidGoal);
            if (!flag)
                return null;
            //BOT
            flag = parser.ValidateExpression(request.BotGoal);
            if (!flag)
                return null;
            //SUPP
            flag = parser.ValidateExpression(request.SuppGoal);
            if (!flag)
                return null;

            return new GoalDB()
            {
                TopGoal = request.TopGoal,
                JungleGoal = request.JungleGoal,
                MidGoal = request.MidGoal,
                BotGoal = request.BotGoal,
                SuppGoal = request.SuppGoal,
                DisplayName = request.DisplayName,
                GoalID = Guid.NewGuid()
            };
        }
    }
}