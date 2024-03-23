using UnityEngine;

namespace RovioAsteroids.MonoBehaviors
{
    public class LaserEnemy : MonoBehaviour, ILaser
    {
        void Start()
        {
            AddForce(300f);
            Destroy(gameObject, 1f);
        }

        public void AddForce(float force)
        {
            GetComponent<Rigidbody2D>().AddForce(transform.up * force);
        }
    }
}
