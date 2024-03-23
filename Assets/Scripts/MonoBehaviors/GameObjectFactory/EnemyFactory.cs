using RovioAsteroids.MonoBehaviors.Abstraction;
using RovioAsteroids.MonoBehaviors.GameObjectFactories.Abstraction;
using RovioAsteroids.Utils;
using System;
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
        private readonly IAddressableLoader _addressableLoader;

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

    public interface IGameObjectFactory<in TParam1, in TParam2, in TParam3, out TValue> : IFactory<string, Vector3, Quaternion, IAddresableGameObject>
    {

    }

    public class CustomGameObjectFactory : IGameObjectFactory<string, Vector3, Quaternion, IAddresableGameObject>
    {
        private readonly DiContainer _diContainer;
        private readonly IAddressableLoader _addressableLoader;

        [Inject]
        public CustomGameObjectFactory(DiContainer diContainer,
            AddressableLoader addressableLoader)
        {
            _diContainer = diContainer;
            _addressableLoader = addressableLoader;
        }

        public IAddresableGameObject Create(string param1, Vector3 param2, Quaternion param3)
        {
            //var addressableGameObject = _addressableLoader.LoadGameObject(param1);
            //var enemy = _diContainer.InstantiatePrefabForComponent<IAddresableGameObject>(addressableGameObject, param2, param3, null);
            //_addressableLoader.ReleaseGameObject(param1);
            //return enemy;
            try
            {
                var addressableGameObject = _addressableLoader.LoadGameObject(param1);
                var createdGameObject = _diContainer.InstantiatePrefabForComponent<IAddresableGameObject>(addressableGameObject, param2, param3, null);
                _addressableLoader.ReleaseGameObject(param1);
                return createdGameObject;
            }
            catch (TypeLoadException)
            {
                return null;
            }
        }
    }

    public class GameObjectFactory : PlaceholderFactory<string, Vector3, Quaternion, IAddresableGameObject>
    {
        
    }

    //////LASERS
    //public interface IRovioLaserFactory<in TParam1, in TParam2, in TParam3, out TValue> : IFactory<string, Vector3, Quaternion, IAddresableGameObject>
    //{

    //}

    //public class RovioCustomLaserFactory : IRovioLaserFactory<string, Vector3, Quaternion, IAddresableGameObject>
    //{
    //    private readonly DiContainer _diContainer;
    //    private readonly IAddressableLoader _addressableLoader;

    //    [Inject]
    //    public RovioCustomLaserFactory(DiContainer diContainer,
    //        AddressableLoader addressableLoader)
    //    {
    //        _diContainer = diContainer;
    //        _addressableLoader = addressableLoader;
    //    }

    //    public IAddresableGameObject Create(string param1, Vector3 param2, Quaternion param3)
    //    {
    //        try
    //        {
    //            var addressableGameObject = _addressableLoader.LoadGameObject(param1);
    //            var laser = _diContainer.InstantiatePrefabForComponent<IAddresableGameObject>(addressableGameObject, param2, param3, null);
    //            _addressableLoader.ReleaseGameObject(param1);
    //            return laser;
    //        }
    //        catch(TypeLoadException)
    //        {
    //            return null;
    //        }
    //    }
    //}

    //public class NewLaserFactory : PlaceholderFactory<string, Vector3, Quaternion, IAddresableGameObject>
    //{

    //}
    //////LASERS

    public interface IAddressableLoader
    {
        GameObject LoadGameObject(string addressableKey);
        void ReleaseGameObject(string addressableKey);
    }

    public class AddressableLoader : IAddressableLoader
    {
        public GameObject LoadGameObject(string addressableKey)
        {
            return AddressablesManager.LoadAssetSync<GameObject>(addressableKey);
        }

        public void ReleaseGameObject(string addressableKey)
        {
            AddressablesManager.ReleaseAsset(addressableKey);
        }
    }
}
