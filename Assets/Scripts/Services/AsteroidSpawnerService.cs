using RovioAsteroids.MonoBehaviors;
using RovioAsteroids.MonoBehaviors.GameObjectFactory.Abstraction;
using RovioAsteroids.Repository.Items.DataModels;
using RovioAsteroids.Repository.Repositories.Abstraction;
using RovioAsteroids.Services.Abstraction;
using RovioAsteroids.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace RovioAsteroids.Services
{
    public class AsteroidSpawnerService : IAsteroidSpawnerService
    {
        private readonly List<Asteroid> _asteroids = new List<Asteroid>();

        private readonly MapHelper _mapHelper;
        private readonly IAsteroidFactory _asteroidFactory;
        private IRepository<AsteroidData> _asteroidDataRepository;

        private AsteroidSpawnerService(
            MapHelper mapHelper,
            InMemoryRepositoryFactory inMemoryRepositoryFactory,
            IAsteroidFactory asteroidFactory)
        {
            _mapHelper = mapHelper;
            _asteroidFactory = asteroidFactory;
            _asteroidDataRepository = inMemoryRepositoryFactory.RepositoryOf<AsteroidData>();
        }

        public void DestroyAllAsteroids()
        {
            for (int i = 0; i < _asteroids.Count; i++)
            {
                GameObject.Destroy(_asteroids[i].gameObject);
                _asteroidDataRepository.Delete(_asteroids[i].UniqueId.ToString());
            }
            _asteroids.Clear();
        }

        public void DestroyAsteroid(Asteroid asteroid)
        {
            Asteroid asteroidToDestroy = FindAsteroid(asteroid.UniqueId);
            if (asteroidToDestroy != null)
            {
                GameObject.Destroy(asteroid.gameObject);
                _asteroidDataRepository.Delete(asteroid.UniqueId.ToString());

                _asteroids.Remove(asteroid);
            }
        }

        public Asteroid FindAsteroid(string uniqueId)
        {
            return _asteroids.Find(x => x.UniqueId == uniqueId);
        }

        public void SpawnAtStart(int defaultNumberOfAsteroidsToSpawn, int spawnMultiplier = 1)
        {
            DestroyAllAsteroids();

            for (int i = 0; i < defaultNumberOfAsteroidsToSpawn * spawnMultiplier; i++)
            {
                SpawnLargeAsteroid();
            }
        }

        public void SpawnLargeAsteroid()
        {
            var asteroid = _asteroidFactory.CreateLargeAsteroid();

            asteroid.transform.position = new Vector3(
                        Random.Range(_mapHelper.Left, _mapHelper.Right),
                        Random.Range(_mapHelper.Bottom, _mapHelper.Top),
                        0);
            asteroid.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-0.0f, 359.0f));

            _asteroids.Add(asteroid);

            _asteroidDataRepository.Create(new
                AsteroidData
            {
                Id = asteroid.UniqueId.ToString(),
                AsteroidUniqueId = asteroid.UniqueId
            });
        }

        public void SpawnSmallFromLarge(Vector3 largeAsteroidPosition)
        {
            float randomX = Random.Range(-0.5f, 0.5f);
            float randomY = Random.Range(-0.5f, 0.5f);

            var asteroid = _asteroidFactory.CreateSmallAsteroid();

            asteroid.transform.position = new Vector3(
                        largeAsteroidPosition.x + randomX,
                        largeAsteroidPosition.y + randomY,
                        0);
            asteroid.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-0.0f, 359.0f));

            _asteroids.Add(asteroid);

            _asteroidDataRepository.Create(new
                AsteroidData
            {
                Id = asteroid.UniqueId.ToString(),
                AsteroidUniqueId = asteroid.UniqueId
            });
        }
    }
}
