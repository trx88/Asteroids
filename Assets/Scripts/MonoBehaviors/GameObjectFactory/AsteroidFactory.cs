using RovioAsteroids.MonoBehaviors.GameObjectFactory.Abstraction;
using RovioAsteroids.Utils;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace RovioAsteroids.MonoBehaviors.GameObjectFactory
{
    public class AsteroidFactory : IAsteroidFactory
    {
        private readonly DiContainer _diContainer;

        private GameObject _asteroidLargeFromAddressable;
        private GameObject _asteroidSmallFromAddressable;

        [Inject]
        public AsteroidFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;

            _asteroidLargeFromAddressable = AddressablesManager.LoadAssetSync<GameObject>(StaticStrings.Addressable_AsteroidLarge);
            _asteroidSmallFromAddressable = AddressablesManager.LoadAssetSync<GameObject>(StaticStrings.Addressable_AsteroidSmall);
        }

        public AsteroidLarge CreateLargeAsteroid()
        {
            return _diContainer.InstantiatePrefabForComponent<AsteroidLarge>(_asteroidLargeFromAddressable);
        }

        public AsteroidSmall CreateSmallAsteroid()
        {
            return _diContainer.InstantiatePrefabForComponent<AsteroidSmall>(_asteroidSmallFromAddressable);
        }
    }
}
