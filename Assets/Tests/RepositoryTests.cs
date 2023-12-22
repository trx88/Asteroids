using NUnit.Framework;
using RovioAsteroids.MonoBehaviors.GameObjectFactory.Abstraction;
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
        [Inject] private IAsteroidFactory _asteroidFactory;
        [Inject] private InMemoryRepositoryFactory _inMemoryRepositoryFactory;
        private IRepository<AsteroidData> _asteroidDataRepository;

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

            _asteroidDataRepository = _inMemoryRepositoryFactory.RepositoryOf<AsteroidData>();
            _asteroidDataRepository.Create(new AsteroidData { AsteroidUniqueId = asteroid.UniqueId });

            var asteroidData = _asteroidDataRepository.Get(x => true).Single();

            Assert.That(asteroidData, !Is.Null);
        }
    }
}
