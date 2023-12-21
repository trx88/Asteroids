using RovioAsteroids.MonoBehaviors;
using RovioAsteroids.Services.Abstraction;

namespace RovioAsteroids.ChainOfResponsibility
{
    public class AsteroidSmallHandler : Handler<Asteroid>
    {
        private readonly IAsteroidSpawnerService _asteroidSpawnerService;
        private readonly IScoringService _scoringService;

        public AsteroidSmallHandler(
            IAsteroidSpawnerService asteroidSpawnerService,
            IScoringService scoringService
            )
        {
            _asteroidSpawnerService = asteroidSpawnerService;
            _scoringService = scoringService;
        }

        public override void HandleItem(Asteroid item)
        {
            if (item.GetType() == typeof(AsteroidSmall))
            {
                _scoringService.UpdateScore(item);
                _asteroidSpawnerService.DestroyAsteroid(item);
            }
            else
            {
                Successor?.HandleItem(item);
            }
        }
    }
}
