using Oinky.TrainingAppAPI.Models.DB;
using Oinky.TrainingAppAPI.Models.Enums;
using Oinky.TrainingAppAPI.Models.Extensions;
using Oinky.TrainingAppAPI.Models.Result;
using Oinky.TrainingAppAPI.Repositories.Interfaces;
using Oinky.TrainingAppAPI.Services.Interfaces;
using Oinky.TrainingAppAPI.Utils;

namespace Oinky.TrainingAppAPI.Services
{
    public class GoalService : IGoalService
    {
        public GoalService(ILogger<GoalService> logger, IGoalRepo goalRepo, IMatchRepo matchRepo)
        {
            m_goalRepo = goalRepo;
            m_matchRepo = matchRepo;
        }

        public async Task<GoalResultDTO> CalculateGoal(Guid goalGUID, string matchID)
        {
            MatchDB match = await m_matchRepo.GetMatchAsync(matchID);
            GoalDB goal = await m_goalRepo.GetGoalAsync(goalGUID);
            if (match == null || goal == null)
                return null;

            GoalResultDTO result = new GoalResultDTO()
            {
                DisplayName = goal.DisplayName,
                GoalID = goal.GoalID,
                MatchID = matchID,
                Participants = new List<ParticipantGoalResult>()
            };
            foreach (var team in match.Teams)
            {
                foreach (ParticipantDB participant in team.Participants)
                {
                    string goalExpr = null;
                    switch (participant.Role)
                    {
                        case Role.TOP:
                            goalExpr = goal.TopGoal;
                            break;

                        case Role.JUNGLE:
                            goalExpr = goal.JungleGoal;
                            break;

                        case Role.MID:
                            goalExpr = goal.MidGoal;
                            break;

                        case Role.BOT:
                            goalExpr = goal.BotGoal;
                            break;

                        case Role.SUPP:
                            goalExpr = goal.SuppGoal;
                            break;
                    }

                    if (goalExpr == null)
                        return null;

                    MathEquationParser parser = new MathEquationParser();
                    if (!parser.Calculate(goalExpr, match, participant.Puuid, out double goalResult))
                    {
                        return null;
                    }

                    Console.WriteLine("VisionScore for: " + participant.SummonerName);
                    Console.WriteLine("Role: " + participant.Role);
                    Console.WriteLine("Result: " + result);

                    result.Participants.Add(new ParticipantGoalResult()
                    {
                        GoalResult = goalResult,
                        PUUID = participant.Puuid,
                        SummonerName = participant.SummonerName,
                        Role = participant.Role,
                        IsOinky = MatchExtension.CheckIfOinky(participant.SummonerName)
                    });
                }
            }
            return result;
        }

        public async Task<bool> CheckIfGoalExistsAsync(Guid goalGUID)
        {
            GoalDB goal = await m_goalRepo.GetGoalAsync(goalGUID);
            return goal != null;
        }

        public async Task<GoalOverviewDTO> GetOverviewAsync()
        {
            GoalOverviewDTO overview = new GoalOverviewDTO();
            foreach (var goal in await m_goalRepo.GetOverviewAsync())
            {
                overview.DefaultGoals.Add(new SingleGoalDTO()
                {
                    DisplayName = goal.DisplayName,
                    GoalID = goal.GoalID
                });
            }
            return overview;
        }

        private IGoalRepo m_goalRepo;
        private IMatchRepo m_matchRepo;
    }
}