using System.Linq;
using TMPro;
using UnityEngine;
using Zenject;

public class GameController : MonoBehaviour
{
    private int score;
    private int hiscore;
    private int asteroidsRemaining;
    private int lives;
    private int wave;
    private int defaultSpawnNumber = 4;

    private IAsteroidSpawnerService _asteroidSpawnerService;
    private IRepository<GameSessionData> _gameSessionDataRepository;
    private IRepository<AsteroidData> _asteroidDataRepository;

    private GameSessionData _gameSessionData;

    [Inject]
    private void Construct(
        InMemoryRepositoryFactory inMemoryRepositoryFactory,
        IAsteroidSpawnerService asteroidSpawnerService)
    {
        _gameSessionDataRepository = inMemoryRepositoryFactory.RepositoryOf<GameSessionData>();
        _asteroidDataRepository = inMemoryRepositoryFactory.RepositoryOf<AsteroidData>();
        _asteroidSpawnerService = asteroidSpawnerService;
    }

    private void OnAsteroidRemoved(AsteroidData asteroidData)
    {
        //New wave
        if(_asteroidDataRepository.Count() == 0 && _gameSessionData.Lives > 0)
        {
            _gameSessionData.Wave++;
            _gameSessionDataRepository.Update(_gameSessionData);
            _asteroidSpawnerService.SpawnAtStart(defaultSpawnNumber, spawnMultiplier: _gameSessionData.Wave);
        }
    }

    private void OnGameSessionDataChanged(GameSessionData gameSessionData)
    {
        _gameSessionData = gameSessionData;
        //Death
        if (gameSessionData.Lives == 0)
        {
            BeginGame();
        }
    }

    private void Awake()
    {
        _asteroidDataRepository.ItemRemoved += OnAsteroidRemoved;
        _gameSessionDataRepository.ItemChanged += OnGameSessionDataChanged;
    }

    private void OnDestroy()
    {
        _asteroidDataRepository.ItemRemoved -= OnAsteroidRemoved;
        _gameSessionDataRepository.ItemChanged -= OnGameSessionDataChanged;
    }

    // Use this for initialization
    void Start()
    {
        BeginGame();
    }

    // Update is called once per frame
    void Update()
    {
        // Quit if player presses escape
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void BeginGame()
    {
        _asteroidSpawnerService.SpawnAtStart(defaultSpawnNumber);

        _gameSessionData = _gameSessionDataRepository.Get(x => true).Single();
        _gameSessionData.Lives = 3;
        _gameSessionData.Score = 0;
        _gameSessionDataRepository.Update(_gameSessionData);
    }
}
