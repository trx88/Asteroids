using UnityEngine;

namespace RovioAsteroids.MonoBehaviors
{
    public class Laser : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Destroy(gameObject, 1.5f);

            GetComponent<Rigidbody2D>()
                .AddForce(transform.up * 400);
        }
    }
}
