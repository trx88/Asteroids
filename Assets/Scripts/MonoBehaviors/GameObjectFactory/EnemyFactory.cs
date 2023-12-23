using RovioAsteroids.MonoBehaviors.GameObjectFactory.Abstraction;
using RovioAsteroids.Utils;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace RovioAsteroids.MonoBehaviors.GameObjectFactory
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly DiContainer _diContainer;

        private GameObject _asteroidLargeFromAddressable;
        private GameObject _asteroidSmallFromAddressable;
        private GameObject _shipEnemyFromAddressable;

        [Inject]
        public EnemyFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;

            _asteroidLargeFromAddressable = AddressablesManager.LoadAssetSync<GameObject>(StaticStrings.Addressable_AsteroidLarge);
            _asteroidSmallFromAddressable = AddressablesManager.LoadAssetSync<GameObject>(StaticStrings.Addressable_AsteroidSmall);
            _shipEnemyFromAddressable = AddressablesManager.LoadAssetSync<GameObject>(StaticStrings.Addressable_ShipEnemy);
        }

        public AsteroidLarge CreateLargeAsteroid()
        {
            return _diContainer.InstantiatePrefabForComponent<AsteroidLarge>(_asteroidLargeFromAddressable);
        }

        public AsteroidSmall CreateSmallAsteroid()
        {
            return _diContainer.InstantiatePrefabForComponent<AsteroidSmall>(_asteroidSmallFromAddressable);
        }

        public ShipEnemyController CreateEnemyShip()
        {
            return _diContainer.InstantiatePrefabForComponent<ShipEnemyController>(_shipEnemyFromAddressable);
        }
    }
}
