using Newtonsoft.Json;
using RovioAsteroids.Repository.Items.Abstraction;

namespace RovioAsteroids.Repository.Items.DataModels
{
    public class EnemyData : Item, IMemoryItem
    {
        [JsonProperty("enemyUniqueId")] public string EnemyUniqueId { get; set; }
    }
}
