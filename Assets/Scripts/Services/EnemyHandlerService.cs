using RovioAsteroids.ChainOfResponsibility;
using RovioAsteroids.ChainOfResponsibility.Abstraction;
using RovioAsteroids.MonoBehaviors;
using RovioAsteroids.Services.Abstraction;
using System.Collections.Generic;
using System.Linq;

namespace RovioAsteroids.Services
{
    public class EnemyHandlerService : IHandlerService
    {
        private readonly List<IHandler<Enemy>> _enemyHandlers = new List<IHandler<Enemy>>();

        private EnemyHandlerService(
            IEnemySpawnerService enemySpawnerService,
            IScoringService scoringService)
        {
            RegisterHandler(new AsteroidSmallHandler(enemySpawnerService, scoringService));
            RegisterHandler(new AsteroidLargeHandler(enemySpawnerService, scoringService));
            RegisterHandler(new ShipEnemyHandler(enemySpawnerService, scoringService));
        }

        public void Handle(Enemy item)
        {
            _enemyHandlers.First().HandleItem(item);
        }

        public void RegisterHandler(IHandler<Enemy> handler)
        {
            if (_enemyHandlers.Count > 0)
            {
                _enemyHandlers[_enemyHandlers.Count - 1].SetSuccessor(handler);
            }

            _enemyHandlers.Add(handler);
        }
    }
}
