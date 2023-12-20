using UnityEngine;

public interface IAsteroidSpawnerService
{

    void SpawnAtStart(int numberOfAsteroidsToSpawn = 4);

    void DestroyAllAsteroids();

    void DestroyAsteroid(Asteroid asteroid);

    Asteroid FindAsteroid(System.Guid uniqueId);

    void SpawnLargeAsteroid();

    void SpawnSmallFromLarge(Vector3 largeAsteroidPosition);
}
