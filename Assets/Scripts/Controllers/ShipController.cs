using System.Linq;
using UnityEngine;
using Zenject;

public class ShipController : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 100.0f;
    [SerializeField] float thrustForce = 3f;

    [SerializeField] public AudioClip crash = default;
    [SerializeField] public AudioClip shoot = default;

    [SerializeField] public GameObject bullet = default;
    [SerializeField] public Transform GunSystem = default;

    private IRepository<GameSessionData> _gameSessionDataRepository;

    [Inject]
    private void Construct(InMemoryRepositoryFactory inMemoryRepositoryFactory)
    {
        _gameSessionDataRepository = inMemoryRepositoryFactory.RepositoryOf<GameSessionData>();
    }

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        //Rotate ship
        transform.Rotate(0, 0, -Input.GetAxis("Horizontal") *
            rotationSpeed * Time.deltaTime);

        //Thrust ship
        GetComponent<Rigidbody2D>().
            AddForce(transform.up * thrustForce *
                Input.GetAxis("Vertical"));
    }

    private void Update()
    {
        //Moved to regular Update since shooting requires more frequent calls.
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

            //Reset ship
            transform.position = new Vector3(0, 0, 0);
            GetComponent<Rigidbody2D>().
                velocity = new Vector3(0, 0, 0);

            //Reduce lives
            GameSessionData gameSessionData = _gameSessionDataRepository.Get(x => true).Single();
            gameSessionData.Lives--;
            _gameSessionDataRepository.Update(gameSessionData);
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
