using RovioAsteroids.Actions.Abstraction;
using RovioAsteroids.Repository.Items.Abstraction;
using RovioAsteroids.Repository.Repositories;
using RovioAsteroids.Repository.Repositories.Abstraction;
using RovioAsteroids.Repository.Repositories.RepositoryFactories;

namespace RovioAsteroids.Repository.Repositories.RepositoryFactories.Abstraction
{
    public interface IRepositoryFactory<TItemFamily> where TItemFamily : IItem
    {
        IRepository<TItem> RepositoryOf<TItem>() where TItem : class, TItemFamily, new();
        void AddRepositoryConfig(RepositoryConfig config);
    }
}


