using RovioAsteroids.MonoBehaviors;
using RovioAsteroids.MonoBehaviors.GameObjectFactory.Abstraction;
using RovioAsteroids.Repository.Items.DataModels;
using RovioAsteroids.Repository.Repositories.Abstraction;
using RovioAsteroids.Repository.Repositories.RepositoryFactories;
using RovioAsteroids.Services.Abstraction;
using RovioAsteroids.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace RovioAsteroids.Services
{
    public class EnemySpawnerService : IEnemySpawnerService
    {
        private readonly List<Enemy> _enemies = new List<Enemy>();

        private readonly MapHelper _mapHelper;
        private readonly IEnemyFactory _enemyFactory;
        private IRepository<EnemyData> _enemyDataRepository;

        private EnemySpawnerService(
            MapHelper mapHelper,
            InMemoryRepositoryFactory inMemoryRepositoryFactory,
            IEnemyFactory enemyFactory)
        {
            _mapHelper = mapHelper;
            _enemyFactory = enemyFactory;
            _enemyDataRepository = inMemoryRepositoryFactory.RepositoryOf<EnemyData>();
        }

        public void DestroyAllEnemies()
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                GameObject.Destroy(_enemies[i].gameObject);
                _enemyDataRepository.Delete(_enemies[i].UniqueId.ToString());
            }
            _enemies.Clear();
        }

        public void DestroyEnemy(Enemy enemy)
        {
            //Enemy enemyToDestroy = FindEnemy(enemy.UniqueId);
            if (FindEnemy(enemy.UniqueId) != null)
            {
                GameObject.Destroy(enemy.gameObject);
                _enemyDataRepository.Delete(enemy.UniqueId.ToString());

                _enemies.Remove(enemy);
            }
        }

        public Enemy FindEnemy(string uniqueId)
        {
            return _enemies.Find(x => x.UniqueId == uniqueId);
        }

        public void SpawnEnemiesAtStart(int defaultNumberOfAsteroidsToSpawn, int spawnMultiplier = 1)
        {
            DestroyAllEnemies();

            for (int i = 0; i < defaultNumberOfAsteroidsToSpawn * spawnMultiplier; i++)
            {
                SpawnLargeAsteroid();
            }
        }

        public void SpawnLargeAsteroid()
        {
            var largeAsteroid = _enemyFactory.CreateLargeAsteroid();

            largeAsteroid.transform.position = new Vector3(
                        Random.Range(_mapHelper.Left, _mapHelper.Right),
                        Random.Range(_mapHelper.Bottom, _mapHelper.Top),
                        0);
            largeAsteroid.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-0.0f, 359.0f));

            _enemies.Add(largeAsteroid);

            _enemyDataRepository.Create(new
                EnemyData
            {
                Id = largeAsteroid.UniqueId.ToString(),
                EnemyUniqueId = largeAsteroid.UniqueId
            });
        }

        public void SpawnSmallAsteroidFromLargeAsteroid(Vector3 largeAsteroidPosition)
        {
            float randomX = Random.Range(-0.5f, 0.5f);
            float randomY = Random.Range(-0.5f, 0.5f);

            var smallAsteroid = _enemyFactory.CreateSmallAsteroid();

            smallAsteroid.transform.position = new Vector3(
                        largeAsteroidPosition.x + randomX,
                        largeAsteroidPosition.y + randomY,
                        0);
            smallAsteroid.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-0.0f, 359.0f));

            _enemies.Add(smallAsteroid);

            _enemyDataRepository.Create(new
                EnemyData
            {
                Id = smallAsteroid.UniqueId.ToString(),
                EnemyUniqueId = smallAsteroid.UniqueId
            });
        }
    }
}
