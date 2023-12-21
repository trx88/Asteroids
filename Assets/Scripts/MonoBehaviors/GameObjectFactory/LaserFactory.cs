using RovioAsteroids.MonoBehaviors.GameObjectFactory.Abstraction;
using RovioAsteroids.Utils;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace RovioAsteroids.MonoBehaviors.GameObjectFactory
{
    public class LaserFactory : ILaserFactory
    {
        private readonly DiContainer _diContainer;

        private GameObject _laserFromAddressable;

        [Inject]
        public LaserFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;

            _laserFromAddressable = AddressablesManager.LoadAssetSync<GameObject>(StaticStrings.Addressable_Laser);
        }

        public Laser CreateLaser(Vector3 spawnPosition, Quaternion spawnRotation)
        {
            return _diContainer.InstantiatePrefabForComponent<Laser>(_laserFromAddressable, spawnPosition, spawnRotation, null);
        }
    }
}
