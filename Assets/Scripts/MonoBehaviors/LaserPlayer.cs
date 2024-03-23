using UnityEngine;

namespace RovioAsteroids.MonoBehaviors
{
    public class LaserPlayer : MonoBehaviour, ILaser
    {
        void Start()
        {
            AddForce(400f);
            Destroy(gameObject, 1.5f);
        }

        public void AddForce(float force)
        {
            GetComponent<Rigidbody2D>().AddForce(transform.up * force);
        }
    }
}
