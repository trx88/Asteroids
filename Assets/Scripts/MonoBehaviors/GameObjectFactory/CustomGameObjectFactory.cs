using RovioAsteroids.MonoBehaviors.Abstraction;
using RovioAsteroids.MonoBehaviors.GameObjectFactories.Abstraction;
using RovioAsteroids.Utils;
using System;
using UnityEngine;
using Zenject;

namespace RovioAsteroids.MonoBehaviors.GameObjectFactories
{
    public class CustomGameObjectFactory : IGameObjectFactory<string, Vector3, Quaternion, IAddresableGameObject>
    {
        private readonly DiContainer _diContainer;
        private readonly AddressableLoader _addressableLoader;

        [Inject]
        public CustomGameObjectFactory(DiContainer diContainer,
            AddressableLoader addressableLoader)
        {
            _diContainer = diContainer;
            _addressableLoader = addressableLoader;
        }

        public IAddresableGameObject Create(string param1, Vector3 param2, Quaternion param3)
        {
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
}
