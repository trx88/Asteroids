using RovioAsteroids.Actions.Abstraction;
using RovioAsteroids.Repository.Items.DataModels;

namespace RovioAsteroids.Actions
{
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
