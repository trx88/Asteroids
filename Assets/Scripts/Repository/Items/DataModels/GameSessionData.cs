using Newtonsoft.Json;
using RovioAsteroids.Repository.Items.Abstraction;

namespace RovioAsteroids.Repository.Items.DataModels
{
    public class GameSessionData : Item, IMemoryItem
    {
        [JsonProperty("score")] public int Score { get; set; }
        [JsonProperty("wave")] public int Wave { get; set; }
        [JsonProperty("lives")] public int Lives { get; set; }
    }
}
