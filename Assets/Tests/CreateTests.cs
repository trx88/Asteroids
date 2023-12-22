using NUnit.Framework;
using RovioAsteroids.MonoBehaviors.GameObjectFactory.Abstraction;
using UnityEngine;
using Zenject;

namespace RovioAsteroids.Tests
{
    [TestFixture]
    public class CreateTests : BaseUnitTest
    {
        [Inject] private IAsteroidFactory _asteroidFactory;
        [Inject] private ILaserFactory _laserFactory;

        [SetUp]
        protected override void Init()
        {
            base.Init();
            Container.Inject(this);
        }

        [Test]
        public void TestCreateSmallAsteroid()
        {
            var asteroid = _asteroidFactory.CreateSmallAsteroid();
            Assert.That(asteroid, !Is.Null);
        }

        [Test]
        public void TestCreateLargeAsteroid()
        {
            var asteroid = _asteroidFactory.CreateLargeAsteroid();
            Assert.That(asteroid, !Is.Null);
        }

        [Test]
        public void TestCreateLaser()
        {
            var laser = _laserFactory.CreateLaser(Vector3.zero, Quaternion.identity);
            Assert.That(laser, !Is.Null);
        }
    }
}
