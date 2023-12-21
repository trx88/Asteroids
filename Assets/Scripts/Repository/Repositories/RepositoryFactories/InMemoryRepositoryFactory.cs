using RovioAsteroids.Actions.Abstraction;
using RovioAsteroids.Repository.Items.Abstraction;

namespace RovioAsteroids.Repository.Repositories.RepositoryFactories
{
    public class InMemoryRepositoryFactory : RepositoryFactory<IMemoryItem>
    {
        protected override Repository<TItem> GenerateRepositoryOf<TItem>(InitializeAction initializeAction)
        {
            return new InMemoryRepository<TItem>(initializeAction);
        }
    }
}
