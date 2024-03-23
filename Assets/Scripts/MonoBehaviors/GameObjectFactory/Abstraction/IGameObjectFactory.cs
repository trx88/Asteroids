using RovioAsteroids.MonoBehaviors.Abstraction;
using UnityEngine;
using Zenject;

namespace RovioAsteroids.MonoBehaviors.GameObjectFactories.Abstraction
{
    public interface IGameObjectFactory<in TParam1, in TParam2, in TParam3, out TValue> : IFactory<string, Vector3, Quaternion, IAddresableGameObject>
    {

    }
}
