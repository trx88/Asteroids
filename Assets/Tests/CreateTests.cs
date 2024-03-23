using NUnit.Framework;
using RovioAsteroids.MonoBehaviors.GameObjectFactories;
using RovioAsteroids.Utils;
using UnityEngine;
using Zenject;

namespace RovioAsteroids.Tests
{
    [TestFixture]
    public class CreateTests : BaseUnitTest
    {
        [Inject] private GameObjectFactory _gameObjectFactory;

        [SetUp]
        protected override void Init()
        {
            base.Init();
            Container.Inject(this);
        }

        [Test]
        public void TestCreateSmallAsteroid()
        {
            var asteroid = _gameObjectFactory.Create(StaticStrings.Addressable_AsteroidSmall, Vector3.zero, Quaternion.identity);
            Assert.That(asteroid, !Is.Null);
        }

        [Test]
        public void TestCreateLargeAsteroid()
        {
            var asteroid = _gameObjectFactory.Create(StaticStrings.Addressable_AsteroidLarge, Vector3.zero, Quaternion.identity);
            Assert.That(asteroid, !Is.Null);
        }

        [Test]
        public void TestCreateLaser()
        {
            var laser = _gameObjectFactory.Create(StaticStrings.Addressable_Laser, Vector3.zero, Quaternion.identity);
            Assert.That(laser, !Is.Null);
        }
    }
}
