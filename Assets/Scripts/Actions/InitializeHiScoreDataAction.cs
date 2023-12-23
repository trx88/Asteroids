using RovioAsteroids.Actions.Abstraction;
using RovioAsteroids.Repository.Items.DataModels;
using RovioAsteroids.Repository.Repositories.RepositoryFactories;

namespace RovioAsteroids.Actions
{
    /// <summary>
    /// Used to initialize a Repository of HiScoreData
    /// </summary>
    public class InitializeHiScoreDataAction : InitializeAction
    {
        private readonly PlayerPrefsRepositoryFactory _playerPrefsRepositoryFactory;

        public InitializeHiScoreDataAction(PlayerPrefsRepositoryFactory repositoryFactory)
        {
            _playerPrefsRepositoryFactory = repositoryFactory;
        }

        public override void Invoke()
        {
            _playerPrefsRepositoryFactory.RepositoryOf<HiScoreData>().Create(
                new HiScoreData
                {
                    HiScore = 0
                });
        }
    }
}
