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

    private AsteroidController _asteroidController;

    [Inject]
    private void Construct(AsteroidController asteroidController)
    {
        _asteroidController = asteroidController;
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

        //SpawnAsteroids();
        //DestroyExistingAsteroids();
        //_asteroidController.DestroyAllAsteroids();
        _asteroidController.SpawnAtStart();
    }

    void SpawnAsteroids()
    {
        DestroyExistingAsteroids();

        // Decide how many asteroids to spawn
        // If any asteroids left over from previous game, subtract them
        asteroidsRemaining = (wave * increaseEachWave);

        for (int i = 0; i < asteroidsRemaining; i++)
        {
            // Spawn an asteroid
            //Instantiate(
            //    asteroid,
            //    new Vector3(
            //        Random.Range(_leftBoundry, _rightBoundry),
            //        Random.Range(_bottomBoundry, _topBoundry),
            //        0),
            //    Quaternion.Euler(0, 0, Random.Range(-0.0f, 359.0f)));
            //_diContainer.InstantiatePrefab(
            //    asteroid,
            //    new Vector3(
            //        Random.Range(_leftBoundry, _rightBoundry),
            //        Random.Range(_bottomBoundry, _topBoundry),
            //        0),
            //    Quaternion.Euler(0, 0, Random.Range(-0.0f, 359.0f)),
            //    null);
        }

        waveText.text = "WAVE: " + wave;
    }

    public void IncrementScore()
    {
        score++;

        scoreText.text = "SCORE:" + score;

        if (score > hiscore)
        {
            hiscore = score;
            hiscoreText.text = "HISCORE: " + hiscore;

            // Save the new hiscore
            PlayerPrefs.SetInt("hiscore", hiscore);
        }

        // Has player destroyed all asteroids?
        if (asteroidsRemaining < 1)
        {

            // Start next wave
            wave++;
            SpawnAsteroids();

        }
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

    public void DecrementAsteroids()
    {
        asteroidsRemaining--;
    }

    public void SplitAsteroid()
    {
        // Two extra asteroids
        // - big one
        // + 3 little ones
        // = 2
        asteroidsRemaining += 2;

    }

    void DestroyExistingAsteroids()
    {
        GameObject[] asteroids =
            GameObject.FindGameObjectsWithTag("AsteroidLarge");

        foreach (GameObject current in asteroids)
        {
            GameObject.Destroy(current);
        }

        GameObject[] asteroids2 =
            GameObject.FindGameObjectsWithTag("AsteroidSmall");

        foreach (GameObject current in asteroids2)
        {
            GameObject.Destroy(current);
        }
    }
}
