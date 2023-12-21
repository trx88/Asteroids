using Newtonsoft.Json;
using RovioAsteroids.Actions.Abstraction;
using RovioAsteroids.Repository.Items.Abstraction;
using System.Collections.Concurrent;
using UnityEngine;

namespace RovioAsteroids.Repository.Repositories
{
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

        protected override void LoadOrInitializeRepository()
        {
            string playerPrefsEntry = PlayerPrefs.GetString((new TItem() as IPlayerPrefsItem).PlayerPrefsKey, null);
            if (string.IsNullOrEmpty(playerPrefsEntry))
            {
                InitializeAction.Invoke();
            }
            else
            {
                _items = JsonConvert.DeserializeObject<ConcurrentDictionary<string, TItem>>(playerPrefsEntry);
            }
        }
    }
}
