using RovioAsteroids.Services.Abstraction;

namespace RovioAsteroids.ChainOfResponsibility
{
    public class AsteroidLargeHandler : Handler<Asteroid>
    {
        private readonly IAsteroidSpawnerService _asteroidSpawnerService;
        private readonly IScoringService _scoringService;

        public AsteroidLargeHandler(
            IAsteroidSpawnerService asteroidSpawnerService,
            IScoringService scoringService
            )
        {
            _asteroidSpawnerService = asteroidSpawnerService;
            _scoringService = scoringService;
        }

        public override void HandleItem(Asteroid item)
        {
            if (item.GetType() == typeof(AsteroidLarge))
            {
                _scoringService.UpdateScore(item);
                _asteroidSpawnerService.DestroyAsteroid(item);
                for (int i = 0; i < 3; i++)
                {
                    _asteroidSpawnerService.SpawnSmallFromLarge(item.transform.position);
                }
            }
            else
            {
                Successor?.HandleItem(item);
            }
        }
    }
}
