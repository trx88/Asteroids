using UnityEngine;

namespace RovioAsteroids.MonoBehaviors.GameObjectFactory.Abstraction
{
    public interface ILaserFactory
    {
        Laser CreateLaser(Vector3 spawnPosition, Quaternion spawnRotation);
    }
}
