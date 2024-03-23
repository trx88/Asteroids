using UnityEngine;

namespace RovioAsteroids.MonoBehaviors.GameObjectFactories.Abstraction
{
    public interface ILaserFactory
    {
        Laser CreateLaser(Vector3 spawnPosition, Quaternion spawnRotation);
        Laser CreateEnemyLaser(Vector3 spawnPosition, Quaternion spawnRotation);
    }
}
