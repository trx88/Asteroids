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

    private void AsteroidCollision(Asteroid asteroid, GameObject other)
    {
        if (asteroid != null)
        {
            //Handle any type of Asteroid (spawn/destroy or just destroy, update score).
            _handlerService.Handle(asteroid);

            //Destroy laser that collided with the asteroid.
            GameObject.Destroy(other.gameObject);
        }
    }
}
