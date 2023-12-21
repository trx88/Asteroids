using UnityEngine;

public interface IAsteroidSpawnerService
{

    void SpawnAtStart(int defaultNumberOfAsteroidsToSpawn, int spawnMultiplier = 1);

    void DestroyAllAsteroids();

    void DestroyAsteroid(Asteroid asteroid);

    Asteroid FindAsteroid(string uniqueId);

    void SpawnLargeAsteroid();

    void SpawnSmallFromLarge(Vector3 largeAsteroidPosition);
}
