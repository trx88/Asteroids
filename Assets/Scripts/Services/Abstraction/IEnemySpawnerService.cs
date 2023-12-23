using RovioAsteroids.MonoBehaviors;
using UnityEngine;

namespace RovioAsteroids.Services.Abstraction
{
    public interface IEnemySpawnerService
    {
        void SpawnEnemiesAtStart(int defaultNumberOfAsteroidsToSpawn, int spawnMultiplier = 1);

        void DestroyAllEnemies();

        void DestroyEnemy(Enemy asteroid);

        Enemy FindEnemy(string uniqueId);

        void SpawnLargeAsteroid();

        void SpawnSmallAsteroidFromLargeAsteroid(Vector3 largeAsteroidPosition);
    }
}
