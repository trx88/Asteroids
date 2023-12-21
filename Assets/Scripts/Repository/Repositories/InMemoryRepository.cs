using RovioAsteroids.Repository.Items.Abstraction;
using System.Collections.Concurrent;

namespace RovioAsteroids.Repository.Repositories
{
    public class InMemoryRepository<TItem> : Repository<TItem> where TItem : class, IItem
    {
        public InMemoryRepository(InitializeAction initializeAction) : base(initializeAction)
        {

        }

        protected override void LoadOrInitializeRepository()
        {
            if (InitializeAction != null)
            {
                InitializeAction.Invoke();
            }
            else
            {
                _items = new ConcurrentDictionary<string, TItem>();
            }
        }
    }
}
