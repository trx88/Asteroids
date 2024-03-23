using RovioAsteroids.MonoBehaviors.GameObjectFactories;
using RovioAsteroids.Utils;
using System.Collections;
using UnityEngine;
using Zenject;

namespace RovioAsteroids.MonoBehaviors
{
    /// <summary>
    /// Controls the enemy ship.
    /// </summary>
    public class ShipEnemyController : Enemy
    {
        public new string AddressableKey => StaticStrings.Addressable_AsteroidLarge;

        [SerializeField] private float _rotationSpeed = 10.0f;
        [SerializeField] private float _firingIntervalInSeconds = 1.5f;
        [SerializeField] private AudioClip _soundCrash = default;
        [SerializeField] private AudioClip _soundShoot = default;
        [SerializeField] private GameObject _laser = default;
        [SerializeField] private Transform _gunSystem = default;

        private Transform _playerShip = default;
        private float timer = 0f;

        private GameObjectFactory _gameObjectFactory;

        [Inject]
        private void Construct(
            GameObjectFactory gameObjectFactory)
        {
            _gameObjectFactory = gameObjectFactory;
        }

        protected override void Init()
        {
            //Don't want push and spin for enemy ship.
        }

        void Start()
        {
            GameObject _player = GameObject.FindWithTag("Player");
            if (_player != null)
            {
                _playerShip = GameObject.FindWithTag("Player").transform;
                StartCoroutine(EnemyShipCoroutine());
            }
        }

        private void OnDestroy()
        {
            StopCoroutine(EnemyShipCoroutine());
        }

        //Avoiding the usage of Update when possible
        private IEnumerator EnemyShipCoroutine()
        {
            while (true)
            {
                Vector3 direction = _playerShip.position - transform.position;

                transform.up = Vector3.Lerp(transform.up, direction, Time.deltaTime * _rotationSpeed);

                timer += Time.deltaTime;
                if (timer % 60 >= _firingIntervalInSeconds)
                {
                    timer = 0f;
                    _gameObjectFactory.Create(
                        StaticStrings.Addressable_LaserEnemy,
                        _gunSystem.position,
                        transform.rotation);
                }

                yield return 0;
            }
        }
    }
}
