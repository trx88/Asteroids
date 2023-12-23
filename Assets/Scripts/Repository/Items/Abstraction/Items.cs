using Newtonsoft.Json;

namespace RovioAsteroids.Repository.Items.Abstraction
{
    public interface IItem
    {
        string Id { get; set; }
    }

    /// <summary>
    /// Base entity for storage. It's a JSON, since JSON is easy to work with, and massive models can serialized with ease.
    /// Id is generated when creating an Item, but in some cases is overridden (like when storing enemies).
    /// Enemies have unique ids, and those should be used as id's in the Repository as well.
    /// </summary>
    public abstract class Item : IItem
    {
        public Item() => Type = GetType().Name;

        [JsonProperty("id")] public string Id { get; set; } = System.Guid.NewGuid().ToString();
        [JsonProperty("type")] public string Type { get; set; }
    }

    public interface IMemoryItem : IItem
    {

    }

    /// <summary>
    /// PlayerPrefs specific Item. PlayerPrefsKey is needed for accessing the data, 
    /// but it shouldn't be stored so it's ignored.
    /// </summary>
    public interface IPlayerPrefsItem : IItem
    {
        [JsonIgnore] public string PlayerPrefsKey { get; }
    }
}
