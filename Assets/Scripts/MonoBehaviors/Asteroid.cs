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
            //Push and spin the asteroid
            GetComponent<Rigidbody2D>()
                .AddForce(transform.up * Random.Range(-50.0f, 150.0f));
            GetComponent<Rigidbody2D>()
                .angularVelocity = Random.Range(-0.0f, 90.0f);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            //TODO: Check for type
            if (other.gameObject.tag.Equals("Laser"))
            {
                _signalBus.Fire(new AsteroidCollisionSignal(this, other.gameObject));
            }
        }
    }
}
