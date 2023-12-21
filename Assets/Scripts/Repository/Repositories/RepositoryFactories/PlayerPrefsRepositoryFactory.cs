using RovioAsteroids.Actions.Abstraction;
using RovioAsteroids.Repository.Items.Abstraction;

namespace RovioAsteroids.Repository.Repositories.RepositoryFactories
{
    public class PlayerPrefsRepositoryFactory : RepositoryFactory<IPlayerPrefsItem>
    {
        protected override Repository<TItem> GenerateRepositoryOf<TItem>(InitializeAction initializeAction)
        {
            return new PlayerPrefsRepository<TItem>(initializeAction);
        }
    }
}
