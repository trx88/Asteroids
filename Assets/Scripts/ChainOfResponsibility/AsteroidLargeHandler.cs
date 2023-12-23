using RovioAsteroids.MonoBehaviors;
using RovioAsteroids.Services.Abstraction;

namespace RovioAsteroids.ChainOfResponsibility
{
    public class AsteroidLargeHandler : Handler<Enemy>
    {
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
                for (int i = 0; i < 3; i++)
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
