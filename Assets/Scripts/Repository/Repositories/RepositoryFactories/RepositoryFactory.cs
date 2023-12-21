using RovioAsteroids.Actions.Abstraction;
using RovioAsteroids.Repository.Items.Abstraction;
using RovioAsteroids.Repository.Repositories.Abstraction;
using RovioAsteroids.Repository.Repositories.RepositoryFactories.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace RovioAsteroids.Repository.Repositories.RepositoryFactories
{
    public abstract class RepositoryFactory<TItemFamily> : IRepositoryFactory<TItemFamily> where TItemFamily : IItem
    {
        private readonly IServiceContainer _repositories;
        private readonly Dictionary<Type, RepositoryConfig> _repositoryConfigs;

        protected RepositoryFactory()
        {
            _repositories = new ServiceContainer();
            _repositoryConfigs = new Dictionary<Type, RepositoryConfig>();
        }

        public void AddRepositoryConfig(RepositoryConfig config)
        {
            if (typeof(TItemFamily).IsAssignableFrom(config.ItemType))
            {
                if (_repositoryConfigs.TryGetValue(config.ItemType, out _))
                {
                    throw new Exception();
                }
                else
                {
                    _repositoryConfigs.Add(config.ItemType, config);
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public IRepository<TItem> RepositoryOf<TItem>() where TItem : class, TItemFamily, new()
        {
            if (new TItem() is TItemFamily)
            {
                if (_repositoryConfigs.TryGetValue(typeof(TItem), out RepositoryConfig config))
                {
                    var requestedRepoType = typeof(Repository<TItem>);

                    if (_repositories.GetService(requestedRepoType) is not Repository<TItem> newRepository)
                    {
                        newRepository = GenerateRepositoryOf<TItem>(config.InitializeAction);
                        _repositories.AddService(requestedRepoType, newRepository);
                    }

                    return newRepository;
                }
                else
                {

                }
            }

            throw new Exception();
        }

        protected abstract Repository<TItem> GenerateRepositoryOf<TItem>(InitializeAction initializeAction) where TItem : class, TItemFamily, new();
    }
}
