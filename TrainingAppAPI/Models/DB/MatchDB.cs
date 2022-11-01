﻿using Oinky.TrainingAppAPI.Models.Enums;

namespace Oinky.TrainingAppAPI.Models.DB
{
    public class MatchDB
    {
        public long GameCreation { get; set; }
        public int GameDuration { get; set; }
        public long GameEndTimestamp { get; set; }
        public GameMode GameMode { get; set; }
        public string GameName { get; set; }
        public long GameStartTimestamp { get; set; }
        public string MatchId { get; set; }
        public List<TeamDB> Teams { get; set; }
    }
}