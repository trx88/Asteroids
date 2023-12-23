using RovioAsteroids.MonoBehaviors;
using RovioAsteroids.Services.Abstraction;

namespace RovioAsteroids.ChainOfResponsibility
{
    /// <summary>
    /// Handles logic when small asteroid is destroyed. 
    /// </summary>
    public class AsteroidSmallHandler : Handler<Enemy>
    {
        private readonly IEnemySpawnerService _enemySpawnerService;
        private readonly IScoringService _scoringService;

        public AsteroidSmallHandler(
            IEnemySpawnerService enemySpawnerService,
            IScoringService scoringService
            )
        {
            _enemySpawnerService = enemySpawnerService;
            _scoringService = scoringService;
        }

        public override void HandleItem(Enemy item)
        {
            if (item.GetType() == typeof(AsteroidSmall))
            {
                _scoringService.UpdateScore(item);
                _enemySpawnerService.DestroyEnemy(item);
            }
            else
            {
                Successor?.HandleItem(item);
            }
        }
    }
}
