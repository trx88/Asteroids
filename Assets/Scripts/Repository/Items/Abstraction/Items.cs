using Newtonsoft.Json;

namespace RovioAsteroids.Repository.Items.Abstraction
{
    public interface IItem
    {
        string Id { get; set; }
    }

    public abstract class Item : IItem
    {
        public Item() => Type = GetType().Name;

        [JsonProperty("id")] public string Id { get; set; } = System.Guid.NewGuid().ToString();
        [JsonProperty("type")] public string Type { get; set; }
    }

    public interface IMemoryItem : IItem
    {

    }

    public interface IPlayerPrefsItem : IItem
    {
        [JsonIgnore] public string PlayerPrefsKey { get; }
    }
}
