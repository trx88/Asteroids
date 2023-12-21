using Newtonsoft.Json;
using RovioAsteroids.Repository.Items.Abstraction;

namespace RovioAsteroids.Repository.Items.DataModels
{
    public class HiScoreData : Item, IPlayerPrefsItem
    {
        public string PlayerPrefsKey => "HiScoreData";
        [JsonProperty("hiScore")] public int HiScore { get; set; }
    }
}
