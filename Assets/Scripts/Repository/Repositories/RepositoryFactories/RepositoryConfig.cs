using RovioAsteroids.Actions.Abstraction;
using System;

namespace RovioAsteroids.Repository.Repositories.RepositoryFactories
{
    public class RepositoryConfig
    {
        public Type ItemType { get; }
        public InitializeAction InitializeAction { get; }

        public RepositoryConfig(Type itemType)
        {
            ItemType = itemType;
        }

        public RepositoryConfig(Type itemType, InitializeAction initializeAction) : this(itemType)
        {
            InitializeAction = initializeAction;
        }
    }
}
