using RovioAsteroids.MonoBehaviors.Abstraction;
using RovioAsteroids.Signals;
using UnityEngine;
using Zenject;

namespace RovioAsteroids.MonoBehaviors
{
    public abstract class Enemy : MonoBehaviour, IEnemy
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

        protected virtual void Init()
        {
            //Push and spin
            GetComponent<Rigidbody2D>()
                .AddForce(transform.up * Random.Range(-50.0f, 150.0f));
            GetComponent<Rigidbody2D>()
                .angularVelocity = Random.Range(-0.0f, 90.0f);
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag.Contains("Laser"))
            {
                _signalBus.Fire(new EnemyCollisionSignal(this, other.gameObject));
            }
        }
    }
}
