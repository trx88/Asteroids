using RovioAsteroids.MonoBehaviors.GameObjectFactories.Abstraction;
using RovioAsteroids.Utils;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace RovioAsteroids.MonoBehaviors.GameObjectFactories
{
    /// <summary>
    /// Used to instantiate enemy game objects using Zenject.
    /// </summary>
    public class EnemyFactory : IEnemyFactory
    {
        private readonly DiContainer _diContainer;
        private readonly AddressableLoader _addressableLoader;

        private GameObject _asteroidLargeFromAddressables;
        private GameObject _asteroidSmallFromAddressables;
        private GameObject _shipEnemyFromAddressables;

        [Inject]
        public EnemyFactory(DiContainer diContainer,
            AddressableLoader addressableLoader)
        {
            _diContainer = diContainer;
            _addressableLoader = addressableLoader;

            _asteroidLargeFromAddressables = AddressablesManager.LoadAssetSync<GameObject>(StaticStrings.Addressable_AsteroidLarge);
            _asteroidSmallFromAddressables = AddressablesManager.LoadAssetSync<GameObject>(StaticStrings.Addressable_AsteroidSmall);
            _shipEnemyFromAddressables = AddressablesManager.LoadAssetSync<GameObject>(StaticStrings.Addressable_ShipEnemy);
        }

        public AsteroidLarge CreateLargeAsteroid()
        {
            return _diContainer.InstantiatePrefabForComponent<AsteroidLarge>(_asteroidLargeFromAddressables);
        }

        public AsteroidSmall CreateSmallAsteroid()
        {
            return _diContainer.InstantiatePrefabForComponent<AsteroidSmall>(_asteroidSmallFromAddressables);
        }

        public ShipEnemyController CreateEnemyShip()
        {
            return _diContainer.InstantiatePrefabForComponent<ShipEnemyController>(_shipEnemyFromAddressables);
        }

        public T CreateEnemy<T>()
        {
            var addressableGameObject = _addressableLoader.LoadGameObject(StaticStrings.Addressable_AsteroidSmall);
            //return _diContainer.InstantiatePrefabForComponent<T>(_asteroidSmallFromAddressables);
            var smallAsteroid = _diContainer.InstantiatePrefabForComponent<T>(addressableGameObject);
            _addressableLoader.ReleaseGameObject(StaticStrings.Addressable_AsteroidSmall);
            return smallAsteroid;
        }
    }
}
