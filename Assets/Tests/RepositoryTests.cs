using NUnit.Framework;
using RovioAsteroids.MonoBehaviors;
using RovioAsteroids.MonoBehaviors.GameObjectFactories;
using RovioAsteroids.Repository.Items.DataModels;
using RovioAsteroids.Repository.Repositories.Abstraction;
using RovioAsteroids.Repository.Repositories.RepositoryFactories;
using RovioAsteroids.Utils;
using System.Linq;
using UnityEngine;
using Zenject;

namespace RovioAsteroids.Tests
{
    [TestFixture]
    public class RepositoryTests : BaseUnitTest
    {
        [Inject] private GameObjectFactory _gameObjectFactory;
        [Inject] private InMemoryRepositoryFactory _inMemoryRepositoryFactory;
        private IRepository<EnemyData> _asteroidDataRepository;

        [SetUp]
        protected override void Init()
        {
            base.Init();
            Container.Inject(this);
        }

        [Test]
        public void TestCreateAsteroidAndSaveToRepo()
        {
            var asteroid = _gameObjectFactory.Create(StaticStrings.Addressable_AsteroidSmall, Vector3.zero, Quaternion.identity) as AsteroidSmall;

            _asteroidDataRepository = _inMemoryRepositoryFactory.RepositoryOf<EnemyData>();
            _asteroidDataRepository.Create(new EnemyData { EnemyUniqueId = asteroid.UniqueId });

            var asteroidData = _asteroidDataRepository.Get(x => true).Single();

            Assert.That(asteroidData, !Is.Null);
        }
    }
}
