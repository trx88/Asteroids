using RovioAsteroids.MonoBehaviors;
using UnityEngine;

namespace RovioAsteroids.Signals
{
    public class EnemyCollisionSignal
    {
        private Enemy _enemy;
        private GameObject _other;

        public EnemyCollisionSignal(Enemy enemy, GameObject other)
        {
            Enemey = enemy;
            Other = other;
        }

        public Enemy Enemey { get => _enemy; private set => _enemy = value; }
        public GameObject Other { get => _other; private set => _other = value; }
    }
}
