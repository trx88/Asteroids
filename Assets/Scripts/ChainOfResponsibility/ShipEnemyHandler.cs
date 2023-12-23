using RovioAsteroids.MonoBehaviors;
using RovioAsteroids.Services.Abstraction;

namespace RovioAsteroids.ChainOfResponsibility
{
    public class ShipEnemyHandler : Handler<Enemy>
    {
        private readonly IEnemySpawnerService _enemySpawnerService;
        private readonly IScoringService _scoringService;

        public ShipEnemyHandler(
            IEnemySpawnerService enemySpawnerService,
            IScoringService scoringService
            )
        {
            _enemySpawnerService = enemySpawnerService;
            _scoringService = scoringService;
        }

        public override void HandleItem(Enemy item)
        {
            if (item.GetType() == typeof(ShipEnemyController))
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
