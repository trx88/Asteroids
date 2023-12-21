using RovioAsteroids.ChainOfResponsibility;
using RovioAsteroids.ChainOfResponsibility.Abstraction;
using RovioAsteroids.MonoBehaviors;
using RovioAsteroids.Services.Abstraction;
using System.Collections.Generic;
using System.Linq;

namespace RovioAsteroids.Services
{
    public class AsteroidHandlerService : IHandlerService
    {
        private readonly List<IHandler<Asteroid>> _asteroidHandlers = new List<IHandler<Asteroid>>();

        private AsteroidHandlerService(
            IAsteroidSpawnerService asteroidSpawnerService,
            IScoringService scoringService)
        {
            RegisterHandler(new AsteroidSmallHandler(asteroidSpawnerService, scoringService));
            RegisterHandler(new AsteroidLargeHandler(asteroidSpawnerService, scoringService));
        }

        public void Handle(Asteroid item)
        {
            _asteroidHandlers.First().HandleItem(item);
        }

        public void RegisterHandler(IHandler<Asteroid> handler)
        {
            if (_asteroidHandlers.Count > 0)
            {
                _asteroidHandlers[_asteroidHandlers.Count - 1].SetSuccessor(handler);
            }

            _asteroidHandlers.Add(handler);
        }
    }
}
