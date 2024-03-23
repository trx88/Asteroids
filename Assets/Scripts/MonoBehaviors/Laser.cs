using RovioAsteroids.MonoBehaviors.Abstraction;
using UnityEngine;

namespace RovioAsteroids.MonoBehaviors
{
    public interface ILaser : IAddresableGameObject
    {
        void AddForce(float force);
    }

    public class Laser : MonoBehaviour
    {
        
    }
}
