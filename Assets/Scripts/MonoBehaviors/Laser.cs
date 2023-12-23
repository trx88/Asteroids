using UnityEngine;

namespace RovioAsteroids.MonoBehaviors
{
    public class Laser : MonoBehaviour
    {
        protected virtual void Init()
        {
            GetComponent<Rigidbody2D>()
                .AddForce(transform.up * 400);
        }
    }
}
