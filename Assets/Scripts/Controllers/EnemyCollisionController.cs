using RovioAsteroids.MonoBehaviors;
using RovioAsteroids.Services.Abstraction;
using RovioAsteroids.Signals;
using UnityEngine;
using Zenject;

namespace RovioAsteroids.Controllers
{
    /// <summary>
    /// Handles the collision of Enemy instances by using ChainOfResponsibility handlers. 
    /// </summary>
    public class EnemyCollisionController
    {
        private readonly IHandlerService _handlerService;

        private EnemyCollisionController(
            IHandlerService handlerService,
            SignalBus signalBus)
        {
            _handlerService = handlerService;
            signalBus.Subscribe<EnemyCollisionSignal>(x => EnemyCollision(x.Enemey, x.Other));
        }

        private void EnemyCollision(Enemy enemy, GameObject other)
        {
            if (enemy != null)
            {
                //Handle any type of Enemy (spawn/destroy or just destroy, update score).
                _handlerService.Handle(enemy);

                //Destroy laser that collided with the enemy.
                GameObject.Destroy(other.gameObject);
            }
        }
    }
}
