using UnityEngine;

namespace RovioAsteroids.MonoBehaviors
{
    public class Laser : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            // Set the bullet to destroy itself after 1 seconds
            Destroy(gameObject, 1.5f);

            // Push the bullet in the direction it is facing
            GetComponent<Rigidbody2D>()
                .AddForce(transform.up * 400);
        }
    }
}
