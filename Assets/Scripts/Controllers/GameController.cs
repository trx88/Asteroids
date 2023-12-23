using RovioAsteroids.Repository.Items.DataModels;
using RovioAsteroids.Repository.Repositories.Abstraction;
using RovioAsteroids.Repository.Repositories.RepositoryFactories;
using RovioAsteroids.Services.Abstraction;
using System.Linq;
using UnityEngine;
using Zenject;

namespace RovioAsteroids.Controllers
{
    /// <summary>
    /// "Main" class. Handles core game logic like resetting a level, ending a game, etc.
    /// </summary>
    public class GameController : MonoBehaviour
    {
        [SerializeField] private int defaultSpawnNumber = 4;

        private IEnemySpawnerService _enemySpawnerService;
        private IRepository<GameSessionData> _gameSessionDataRepository;
        private IRepository<EnemyData> _enemyDataRepository;

        private GameSessionData _gameSessionData;

        [Inject]
        private void Construct(
            InMemoryRepositoryFactory inMemoryRepositoryFactory,
            IEnemySpawnerService enemySpawnerService)
        {
            _gameSessionDataRepository = inMemoryRepositoryFactory.RepositoryOf<GameSessionData>();
            _enemyDataRepository = inMemoryRepositoryFactory.RepositoryOf<EnemyData>();
            _enemySpawnerService = enemySpawnerService;
        }

        private void Awake()
        {
            //Subscribing to item removal from the Repository
            _enemyDataRepository.ItemRemoved += OnEnemyRemoved;
            _gameSessionDataRepository.ItemChanged += OnGameSessionDataChanged;
        }

        private void OnDestroy()
        {
            //Unsubscribing to item removal from the Repository
            _enemyDataRepository.ItemRemoved -= OnEnemyRemoved;
            _gameSessionDataRepository.ItemChanged -= OnGameSessionDataChanged;
        }

        void Start()
        {
            BeginGame();
        }

        void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

        private void BeginGame()
        {
            _enemySpawnerService.SpawnEnemiesAtStart(defaultSpawnNumber);

            _gameSessionData = _gameSessionDataRepository.Get(x => true).Single();
            _gameSessionData.Lives = 3;
            _gameSessionData.Score = 0;
            _gameSessionData.Wave = 1;
            _gameSessionDataRepository.Update(_gameSessionData);
        }

        private void OnEnemyRemoved(EnemyData asteroidData)
        {
            //New wave
            if (_enemyDataRepository.Count() == 0 && _gameSessionData.Lives > 0)
            {
                _gameSessionData.Wave++;
                _gameSessionDataRepository.Update(_gameSessionData);
                _enemySpawnerService.SpawnEnemiesAtStart(defaultSpawnNumber, spawnMultiplier: _gameSessionData.Wave);
            }
        }

        private void OnGameSessionDataChanged(GameSessionData gameSessionData)
        {
            _gameSessionData = gameSessionData;
            //Game over
            if (gameSessionData.Lives == 0)
            {
                BeginGame();
            }
        }
    }
}
