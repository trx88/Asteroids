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
    private int increaseEachWave = 4;

    public GameObject asteroid;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI hiscoreText;

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
            _asteroidSpawnerService.SpawnAtStart();
        }
    }

    private void OnGameSessionDataChanged(GameSessionData gameSessionData)
    {
        if(gameSessionData.Lives == 0)
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
        score = 0;
        lives = 3;
        wave = 1;

        // Prepare the HUD
        scoreText.text = "SCORE:" + score;
        hiscoreText.text = "HISCORE: " + hiscore;
        livesText.text = "LIVES: " + lives;
        waveText.text = "WAVE: " + wave;

        _asteroidSpawnerService.SpawnAtStart();

        _gameSessionData = _gameSessionDataRepository.Get(x => true).Single();
        _gameSessionData.Lives = 3;
        _gameSessionData.Score = 0;
        _gameSessionDataRepository.Update(_gameSessionData);
    }

    public void DecrementLives()
    {
        lives--;
        livesText.text = "LIVES: " + lives;

        // Has player run out of lives?
        if (lives < 1)
        {
            // Restart the game
            BeginGame();
        }
    }
}
