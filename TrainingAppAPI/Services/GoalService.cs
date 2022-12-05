using Microsoft.AspNetCore.Mvc;
using Oinky.TrainingAppAPI.Models.DB;
using Oinky.TrainingAppAPI.Models.Enums;
using Oinky.TrainingAppAPI.Models.Extensions;
using Oinky.TrainingAppAPI.Models.Request;
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

        public async Task<IActionResult> AddGoalAsync(AddGoalRequest request)
        {
            //Convert
            GoalDB goalDB = request.ConvertToDB();
            if (goalDB == null)
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            //Add
            if (await m_goalRepo.AddGoalAsync(goalDB))
                return new OkResult();
            else
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
        }

        public async Task<GoalResultDTO> CalculateGoalAsync(Guid goalID, string matchID)
        {
            MatchDB match = await m_matchRepo.GetMatchAsync(matchID);
            GoalDB goal = await m_goalRepo.GetGoalAsync(goalID);
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

        public async Task<ExtendedGoalDTO> GetGoalAsync(Guid goalID)
        {
            GoalDB goal = await m_goalRepo.GetGoalAsync(goalID);
            return goal == null ? null : goal.ConvertToExtended();
        }

        public async Task<GoalOverviewDTO> GetOverviewAsync()
        {
            GoalOverviewDTO overview = new GoalOverviewDTO();
            List<GoalDB> goals = await m_goalRepo.GetOverviewAsync();
            if (goals != null)
                foreach (var goal in goals)
                {
                    overview.DefaultGoals.Add(new SingleGoalDTO()
                    {
                        DisplayName = goal.DisplayName,
                        GoalID = goal.GoalID
                    });
                }
            return overview;
        }

        public async Task<bool> UpdateGoalAsync(Guid goalID, AddGoalRequest request)
        {
            if (!await CheckIfGoalExistsAsync(goalID))
                return false;
            GoalDB goalDB = request.ConvertToDB();
            goalDB.GoalID = goalID;
            return await m_goalRepo.UpdateGoalAsync(goalDB);
        }

        public async  Task<bool> DeleteGoalAsync(Guid goalID)
        {
            if (!await CheckIfGoalExistsAsync(goalID))
                return false;
            return await m_goalRepo.DeleteGoalAsync(goalID);
        }

        private IGoalRepo m_goalRepo;
        private IMatchRepo m_matchRepo;
    }
}