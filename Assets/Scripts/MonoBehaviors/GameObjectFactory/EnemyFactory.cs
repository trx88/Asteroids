using RovioAsteroids.MonoBehaviors.GameObjectFactory.Abstraction;
using RovioAsteroids.Utils;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace RovioAsteroids.MonoBehaviors.GameObjectFactory
{
    /// <summary>
    /// Used to instantiate enemy game objects using Zenject.
    /// </summary>
    public class EnemyFactory : IEnemyFactory
    {
        private readonly DiContainer _diContainer;

        private GameObject _asteroidLargeFromAddressables;
        private GameObject _asteroidSmallFromAddressables;
        private GameObject _shipEnemyFromAddressables;

        [Inject]
        public EnemyFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;

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
    }
}
