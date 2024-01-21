using Newtonsoft.Json;
using RovioAsteroids.Repository.Items.Abstraction;
using System;

namespace RovioAsteroids.Repository.Items.DataModels
{
    public class GameSessionData : Item, IMemoryItem, ICloneable
    {
        [JsonProperty("score")] public int Score { get; set; }
        [JsonProperty("wave")] public int Wave { get; set; }
        [JsonProperty("lives")] public int Lives { get; set; }

        public override object Clone()
        {
            return new GameSessionData
            {
                Id = Id,
                Type = Type,
                Lives = Lives,
                Score = Score,
                Wave = Wave
            };
        }
    }
}
