using RovioAsteroids.MonoBehaviors.GameObjectFactories.Abstraction;
using RovioAsteroids.Utils;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace RovioAsteroids.MonoBehaviors.GameObjectFactories
{
    /// <summary>
    /// Used to instantiate laser game objects using Zenject.
    /// </summary>
    public class LaserFactory : ILaserFactory
    {
        private readonly DiContainer _diContainer;

        private GameObject _laserFromAddressable;
        private GameObject _laserEnemyFromAddressable;

        [Inject]
        public LaserFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;

            _laserFromAddressable = AddressablesManager.LoadAssetSync<GameObject>(StaticStrings.Addressable_Laser);
            _laserEnemyFromAddressable = AddressablesManager.LoadAssetSync<GameObject>(StaticStrings.Addressable_LaserEnemy);
        }

        public Laser CreateLaser(Vector3 spawnPosition, Quaternion spawnRotation)
        {
            return _diContainer.InstantiatePrefabForComponent<Laser>(_laserFromAddressable, spawnPosition, spawnRotation, null);
        }

        public Laser CreateEnemyLaser(Vector3 spawnPosition, Quaternion spawnRotation)
        {
            return _diContainer.InstantiatePrefabForComponent<Laser>(_laserEnemyFromAddressable, spawnPosition, spawnRotation, null);
        }
    }
}
