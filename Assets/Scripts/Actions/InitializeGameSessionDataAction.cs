using RovioAsteroids.Actions.Abstraction;
using RovioAsteroids.Repository.Items.DataModels;
using RovioAsteroids.Repository.Repositories.RepositoryFactories;

namespace RovioAsteroids.Actions
{
    /// <summary>
    /// Used to initialize a Repository of GameSessionData
    /// </summary>
    public class InitializeGameSessionDataAction : InitializeAction
    {
        private readonly InMemoryRepositoryFactory _inMemoryRepositoryFactory;

        public InitializeGameSessionDataAction(InMemoryRepositoryFactory repositoryFactory)
        {
            _inMemoryRepositoryFactory = repositoryFactory;
        }

        public override void Invoke()
        {
            _inMemoryRepositoryFactory.RepositoryOf<GameSessionData>().Create(
                new GameSessionData
                {
                    Lives = 3,
                    Score = 0,
                    Wave = 1
                });
        }
    }
}
