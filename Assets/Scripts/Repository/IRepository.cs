using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

public abstract class UserData : Item
{
    [JsonProperty("userId")] public string UserId { get; set; }
}

public interface IMemoryItem : IItem
{

}

public interface IPlayerPrefsItem : IItem
{
    [JsonIgnore] public string PlayerPrefsKey { get; }
}

public interface IRepository<TItem> where TItem : class, IItem
{
    public Action<TItem>? ItemAdded { get; set; }
    public Action<TItem>? ItemChanged { get; set; }
    public Action<TItem>? ItemRemoved { get; set; }

    int Count();
    TItem Create(TItem value);
    void Delete(string id);
    bool Exists(string id);
    TItem Get(string id);
    IEnumerable<TItem> Get(Func<TItem, bool> predicate);
    void Update(TItem value);
}

public abstract class Repository<TItem> : IRepository<TItem> where TItem : class, IItem
{
    protected ConcurrentDictionary<string, TItem> _items;

    protected InitializeAction InitializeAction { get; private set; }
    public Action<TItem> ItemAdded { get; set; }
    public Action<TItem> ItemChanged { get; set; }
    public Action<TItem> ItemRemoved { get; set; }

    protected Repository(InitializeAction initializeAction)
    {
        InitializeAction = initializeAction;
    }

    protected abstract void LoadOrInitializeRepozitory();

    public int Count()
    {
        if(_items == null)
        {

        }

        return _items?.Count ?? 0;
    }

    public virtual TItem Create(TItem value)
    {
        _items ??= new ConcurrentDictionary<string, TItem>();

        TItem result = default;

        if(value != null)
        {
            if(!_items.TryAdd(value.Id, value))
            {
                //error
            }

            ItemAdded?.Invoke(result);
        }

        return result;
    }

    public virtual void Delete(string id)
    {
        if(_items == null)
        {
            LoadOrInitializeRepozitory();
        }

        if(_items.TryRemove(id, out TItem removedItem))
        {
            ItemRemoved?.Invoke(removedItem);
        }
        else
        {
            //Not found
        }
    }

    public virtual bool Exists(string id)
    {
        try
        {
            return Get(id) != null;
        }
        catch(KeyNotFoundException)
        {
            return false;
        }
    }

    public virtual TItem Get(string id)
    {
        if (_items == null)
        {
            LoadOrInitializeRepozitory();
        }

        if (_items.TryGetValue(id, out TItem item))
        {
            return item as TItem; //Clone?
        }
        else
        {
            //Not found
            return default;
        }
    }

    public IEnumerable<TItem> Get(Func<TItem, bool> predicate)
    {
        if (_items == null)
        {
            LoadOrInitializeRepozitory();
        }

        return _items.Values.Where(predicate).ToList();
    }

    public virtual void Update(TItem value)
    {
        if (_items == null)
        {
            LoadOrInitializeRepozitory();
        }

        if (_items.TryGetValue(value.Id, out TItem item))
        {
            _items[item.Id] = item;
            ItemChanged?.Invoke(item);
        }
        else
        {
            //Not found
        }
    }
}

public class InMemoryRepository<TItem> : Repository<TItem> where TItem : class, IItem
{
    public InMemoryRepository(InitializeAction initializeAction) : base(initializeAction)
    {

    }

    protected override void LoadOrInitializeRepozitory()
    {
        if(InitializeAction != null)
        {
            InitializeAction.Invoke();
        }
        else
        {
            _items = new ConcurrentDictionary<string, TItem>();
        }
    }
}

public class PlayerPrefsRepository<TItem> : Repository<TItem> where TItem : class, IItem, new()
{
    public PlayerPrefsRepository(InitializeAction initializeAction) : base(initializeAction)
    {

    }

    private void Save()
    {
        PlayerPrefs.SetString(
            (new TItem() as IPlayerPrefsItem).PlayerPrefsKey,
            JsonConvert.SerializeObject(_items));
        PlayerPrefs.Save();
    }

    public override TItem Create(TItem value)
    {
        var result = base.Create(value);
        Save();

        return result;
    }

    public override void Delete(string id)
    {
        base.Delete(id);
        Save();
    }

    public override void Update(TItem value)
    {
        base.Update(value);
        Save();
    }

    protected override void LoadOrInitializeRepozitory()
    {
        string playerPrefsEntry = PlayerPrefs.GetString((new TItem() as IPlayerPrefsItem).PlayerPrefsKey, null);
        if(string.IsNullOrEmpty(playerPrefsEntry))
        {
            InitializeAction.Invoke();
        }
        else
        {
            _items = JsonConvert.DeserializeObject<ConcurrentDictionary<string, TItem>>(playerPrefsEntry);
        }
    }
}

public class HiScoreData : Item, IPlayerPrefsItem
{
    public string PlayerPrefsKey => "HiScoreData";
    [JsonProperty("hiScore")] public int HiScore { get; set; }
}

public class GameSessionData : Item, IMemoryItem
{
    [JsonProperty("score")] public int Score { get; set; }
    [JsonProperty("wave")] public int Wave { get; set; }
    [JsonProperty("lives")] public int Lives { get; set; }
}

public class AsteroidData : Item, IMemoryItem
{
    [JsonProperty] public string AsteroidUniqueId { get; set; }
}
