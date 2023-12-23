using RovioAsteroids.MonoBehaviors;
using RovioAsteroids.Services.Abstraction;

namespace RovioAsteroids.ChainOfResponsibility
{
    /// <summary>
    /// Handles logic when large asteroid is destroyed. 
    /// </summary>
    public class AsteroidLargeHandler : Handler<Enemy>
    {
        private const int _numberOfSmallAsteroidsToSpawn = 3;

        private readonly IEnemySpawnerService _enemySpawnerService;
        private readonly IScoringService _scoringService;

        public AsteroidLargeHandler(
            IEnemySpawnerService enemySpawnerService,
            IScoringService scoringService
            )
        {
            _enemySpawnerService = enemySpawnerService;
            _scoringService = scoringService;
        }

        public override void HandleItem(Enemy item)
        {
            if (item.GetType() == typeof(AsteroidLarge))
            {
                _scoringService.UpdateScore(item);
                _enemySpawnerService.DestroyEnemy(item);
                for (int i = 0; i < _numberOfSmallAsteroidsToSpawn; i++)
                {
                    _enemySpawnerService.SpawnSmallAsteroidFromLargeAsteroid(item.transform.position);
                }
            }
            else
            {
                Successor?.HandleItem(item);
            }
        }
    }
}
