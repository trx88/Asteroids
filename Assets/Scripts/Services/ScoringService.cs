using RovioAsteroids.Services.Abstraction;
using System.Linq;

namespace RovioAsteroids.Services
{
    public class ScoringService : IScoringService
    {
        private IRepository<HiScoreData> _highScoreDataRepository;
        private IRepository<GameSessionData> _gameSessionDataRepository;

        private ScoringService(
            InMemoryRepositoryFactory inMemoryRepositoryFactory,
            PlayerPrefsRepositoryFactory playerPrefsRepositoryFactory)
        {
            _gameSessionDataRepository = inMemoryRepositoryFactory.RepositoryOf<GameSessionData>();
            _highScoreDataRepository = playerPrefsRepositoryFactory.RepositoryOf<HiScoreData>();
        }

        public void UpdateScore(Asteroid asteroid)
        {
            GameSessionData gameSessionData = _gameSessionDataRepository.Get(x => true).Single();
            HiScoreData hiScoreData = _highScoreDataRepository.Get(x => true).Single();
            gameSessionData.Score += asteroid.ScorePoints;

            if (gameSessionData.Score > hiScoreData.HiScore)
            {
                hiScoreData.HiScore = gameSessionData.Score;
            }

            _gameSessionDataRepository.Update(gameSessionData);
            _highScoreDataRepository.Update(hiScoreData);
        }
    }
}
