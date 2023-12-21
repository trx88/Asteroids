using RovioAsteroids.MonoBehaviors.Abstraction;
using RovioAsteroids.Signals;
using UnityEngine;
using Zenject;

namespace RovioAsteroids.MonoBehaviors
{
    public abstract class Asteroid : MonoBehaviour, IAsteroid
    {
        [SerializeField] private AudioClip _destroy = default;
        [SerializeField] private int _scorePoints = default;

        public int ScorePoints { get => _scorePoints; }

        protected string uniqueId;
        public string UniqueId { get => uniqueId; }

        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Awake()
        {
            uniqueId = System.Guid.NewGuid().ToString();
        }

        void OnDestroy()
        {
            AudioSource.PlayClipAtPoint(_destroy, Camera.main.transform.position);
        }

        protected void Init()
        {
            // Push the asteroid in the direction it is facing
            GetComponent<Rigidbody2D>()
                .AddForce(transform.up * Random.Range(-50.0f, 150.0f));

            // Give a random angular velocity/rotation
            GetComponent<Rigidbody2D>()
                .angularVelocity = Random.Range(-0.0f, 90.0f);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag.Equals("Laser"))
            //if (other.gameObject.GetComponent<BulletController>() != null)
            {
                _signalBus.Fire(new AsteroidCollisionSignal(this, other.gameObject));
            }
        }
    }
}
