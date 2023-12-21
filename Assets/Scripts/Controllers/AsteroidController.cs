using RovioAsteroids.Services.Abstraction;
using RovioAsteroids.Signals;
using UnityEngine;
using Zenject;

namespace RovioAsteroids.Controllers
{
    public class AsteroidController
    {
        private readonly IHandlerService _handlerService;

        private AsteroidController(
            IHandlerService handlerService,
            SignalBus signalBus)
        {
            _handlerService = handlerService;
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
}
