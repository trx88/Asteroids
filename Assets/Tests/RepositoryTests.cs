using NUnit.Framework;
using RovioAsteroids.MonoBehaviors.GameObjectFactories.Abstraction;
using RovioAsteroids.Repository.Items.DataModels;
using RovioAsteroids.Repository.Repositories.Abstraction;
using RovioAsteroids.Repository.Repositories.RepositoryFactories;
using System.Linq;
using Zenject;

namespace RovioAsteroids.Tests
{
    [TestFixture]
    public class RepositoryTests : BaseUnitTest
    {
        [Inject] private IEnemyFactory _asteroidFactory;
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
            var asteroid = _asteroidFactory.CreateSmallAsteroid();

            _asteroidDataRepository = _inMemoryRepositoryFactory.RepositoryOf<EnemyData>();
            _asteroidDataRepository.Create(new EnemyData { EnemyUniqueId = asteroid.UniqueId });

            var asteroidData = _asteroidDataRepository.Get(x => true).Single();

            Assert.That(asteroidData, !Is.Null);
        }
    }
}
