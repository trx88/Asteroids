using RovioAsteroids.MonoBehaviors.GameObjectFactory.Abstraction;
using System.Collections;
using UnityEngine;
using Zenject;

namespace RovioAsteroids.MonoBehaviors
{
    public class ShipEnemyController : Enemy
    {
        [SerializeField] private float _rotationSpeed = 10.0f;
        [SerializeField] private float _firingIntervalInSeconds = 1.5f;
        [SerializeField] private AudioClip _soundCrash = default;
        [SerializeField] private AudioClip _soundShoot = default;
        [SerializeField] private GameObject _laser = default;
        [SerializeField] private Transform _gunSystem = default;
        
        private Transform _playerShip = default;
        private float timer = 0f;

        private ILaserFactory _laserFactory;

        [Inject]
        private void Construct(
            ILaserFactory laserFactory)
        {
            _laserFactory = laserFactory;
        }

        protected override void Init()
        {
            //We don't want push and spin for enemy ship.
        }

        // Start is called before the first frame update
        void Start()
        {
            GameObject _player = GameObject.FindWithTag("Player");
            if(_player != null)
            {
                _playerShip = GameObject.FindWithTag("Player").transform;
                StartCoroutine(EnemyShipCoroutine());
            }
        }

        private void OnDestroy()
        {
            StopCoroutine(EnemyShipCoroutine());
        }

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
                    _laserFactory.CreateEnemyLaser(
                        _gunSystem.position,
                        transform.rotation);
                }

                yield return 0;
            }
        }
    }
}
