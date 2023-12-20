using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{ 
    string Id { get; set; }
}

public abstract class Item : IItem
{
    public Item() => Type = GetType().Name;

    public string Id { get; set; } = System.Guid.NewGuid().ToString();
    public string Type { get; set; }
}

public abstract class UserData : Item
{
    public string UserId { get; set; }
}

public interface IMemoryItem : IItem
{

}

public interface IPlayerPrefsItem : IItem
{
    public string PlayerPrefsKey { get; }
}

public interface IRepository<TItem> where TItem : class, IItem
{
    public Action<TItem>? ItemAdded { get; set; }
    public Action<TItem>? ItemChanged { get; set; }
    public Action<TItem>? ItemRemoved { get; set; }

    int Count();
    TItem Create(TItem value);
    void Delete(TItem value);
    bool Exists(string id);
    TItem Get(string id);
    void Updated(TItem value);
}

public abstract class Repository<TItem> : IRepository<TItem> where TItem : class, IItem
{
    protected ConcurrentDictionary<string, TItem> _items;

    public Action<TItem> ItemAdded { get; set; }
    public Action<TItem> ItemChanged { get; set; }
    public Action<TItem> ItemRemoved { get; set; }

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

    public virtual void Delete(TItem value)
    {
        if(_items == null)
        {
            //load
        }

        if(_items.TryRemove(value.Id, out TItem removedItem))
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
            //load
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

    public virtual void Updated(TItem value)
    {
        if (_items == null)
        {
            //load
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

public class PlayerPrefsRepository<TItem> : Repository<TItem> where TItem : class, IItem, new()
{
    private void Save()
    {
        //PlayerPrefs.SetString((new TItem() as IPlayerPrefsItem).PlayerPrefsKey, JsonConvert
        PlayerPrefs.Save();
    }

    public override TItem Create(TItem value)
    {
        var result = base.Create(value);
        Save();

        return result;
    }
}