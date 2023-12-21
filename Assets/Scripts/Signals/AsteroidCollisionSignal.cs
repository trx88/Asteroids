using RovioAsteroids.MonoBehaviors;
using UnityEngine;

namespace RovioAsteroids.Signals
{
    public class AsteroidCollisionSignal
    {
        private Asteroid _asteroid;
        private GameObject _other;

        public AsteroidCollisionSignal(Asteroid asteroid, GameObject other)
        {
            Asteroid = asteroid;
            Other = other;
        }

        public Asteroid Asteroid { get => _asteroid; private set => _asteroid = value; }
        public GameObject Other { get => _other; private set => _other = value; }
    }
}
