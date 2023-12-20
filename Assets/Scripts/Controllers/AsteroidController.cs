using UnityEngine;
using Zenject;

public class AsteroidController
{
    private readonly IHandlerService _handlerService;
    private readonly IAsteroidSpawnerService _asteroidSpawnerService;

    private AsteroidController(
        IHandlerService handlerService,
        IAsteroidSpawnerService asteroidSpawnerService,
        SignalBus signalBus)
    {
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

            GameObject.Destroy(other.gameObject);
        }
    }
}
