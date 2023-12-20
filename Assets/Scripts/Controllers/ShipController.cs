using UnityEngine;
using Zenject;

public class ShipController : MonoBehaviour
{
    float rotationSpeed = 100.0f;
    float thrustForce = 3f;

    public AudioClip crash;
    public AudioClip shoot;

    public GameObject bullet;
    public Transform GunSystem;

    private GameController gameController;

    void Start()
    {
        // Get a reference to the game controller object and the script
        GameObject gameControllerObject =
            GameObject.FindWithTag("GameController");

        gameController =
            gameControllerObject.GetComponent<GameController>();
    }

    void FixedUpdate()
    {
        // Rotate the ship if necessary
        transform.Rotate(0, 0, -Input.GetAxis("Horizontal") *
            rotationSpeed * Time.deltaTime);

        // Thrust the ship if necessary
        GetComponent<Rigidbody2D>().
            AddForce(transform.up * thrustForce *
                Input.GetAxis("Vertical"));
    }

    private void Update()
    {
        // Has a bullet been fired
        if (Input.GetMouseButtonDown(0))
        {
            //ShootBullet();
            ShootThree();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Anything except a bullet is an asteroid
        if (other.gameObject.tag != "Laser")
        {
            AudioSource.PlayClipAtPoint
                (crash, Camera.main.transform.position);

            // Move the ship to the center of the screen
            transform.position = new Vector3(0, 0, 0);

            // Remove all velocity from the ship
            GetComponent<Rigidbody2D>().
                velocity = new Vector3(0, 0, 0);

            gameController.DecrementLives();
        }
    }

    void ShootBullet()
    {
        // Spawn a bullet
        Instantiate(bullet,
            new Vector3(transform.position.x, transform.position.y, 0),
            transform.rotation);

        // Play a shoot sound
        AudioSource.PlayClipAtPoint(shoot, Camera.main.transform.position);
    }

    void ShootThree()
    {
        // Spawn a bullet 1
        Instantiate(bullet,
            new Vector3(GunSystem.position.x, GunSystem.position.y, 0),
            ModifyQuaternionWithEuler(transform.rotation, new Vector3(0f, 0f, -20f)));

        // Spawn a bullet 2
        Instantiate(bullet,
            new Vector3(GunSystem.position.x, GunSystem.position.y, 0),
            transform.rotation);

        // Spawn a bullet 3
        Instantiate(bullet,
            new Vector3(GunSystem.position.x, GunSystem.position.y, 0),
            ModifyQuaternionWithEuler(transform.rotation, new Vector3(0f, 0f, 20f)));

        // Play a shoot sound
        AudioSource.PlayClipAtPoint(shoot, Camera.main.transform.position);
    }

    Quaternion ModifyQuaternionWithEuler(Quaternion rotation, Vector3 euler)
    {
        return Quaternion.Euler(rotation.eulerAngles + euler);
    }
}
