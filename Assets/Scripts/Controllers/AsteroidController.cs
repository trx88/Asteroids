using System.Linq;
using UnityEngine;
using Zenject;

public class AsteroidController
{
    private readonly IHandlerService _handlerService;
    private readonly IAsteroidSpawnerService _asteroidSpawnerService;
    private readonly IRepository<HiScoreData> _hiScoreDataRepository;
    private IRepository<GameSessionData> _gameSessionDataRepository;

    private AsteroidController(
        InMemoryRepositoryFactory inMemoryRepositoryFactory,
        PlayerPrefsRepositoryFactory playerPrefsRepositoryFactory,
        IHandlerService handlerService,
        IAsteroidSpawnerService asteroidSpawnerService,
        SignalBus signalBus)
    {
        _gameSessionDataRepository = inMemoryRepositoryFactory.RepositoryOf<GameSessionData>();
        _hiScoreDataRepository = playerPrefsRepositoryFactory.RepositoryOf<HiScoreData>();
        _handlerService = handlerService;
        _asteroidSpawnerService = asteroidSpawnerService;
        signalBus.Subscribe<AsteroidCollisionSignal>(x => AsteroidCollision(x.Asteroid, x.Other));
    }

    public /*for now*/ void SpawnAtStart()
    {
        _asteroidSpawnerService.SpawnAtStart();
    }

    public /*for now*/ void DestroyAllAsteroids()
    {
        _asteroidSpawnerService.DestroyAllAsteroids();
    }

    private void AsteroidCollision(Asteroid asteroid, GameObject other)
    {
        if (asteroid != null)
        {
            _handlerService.Handle(asteroid);

            var hiScore = _hiScoreDataRepository.Get(x => true).Single();
            hiScore.HiScore++;
            _hiScoreDataRepository.Update(hiScore);
            var hiScoreUpdated = _hiScoreDataRepository.Get(x => true).Single();

            var gameSessionData = _gameSessionDataRepository.Get(x => true).Single();

            GameObject.Destroy(other.gameObject);
        }
    }
}
