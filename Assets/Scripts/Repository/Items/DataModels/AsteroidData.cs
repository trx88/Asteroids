using Newtonsoft.Json;
using RovioAsteroids.Repository.Items.Abstraction;

namespace RovioAsteroids.Repository.Items.DataModels
{
    public class AsteroidData : Item, IMemoryItem
    {
        [JsonProperty("asteroidUniqueId")] public string AsteroidUniqueId { get; set; }
    }
}
