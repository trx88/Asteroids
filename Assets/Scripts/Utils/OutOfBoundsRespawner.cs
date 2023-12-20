using UnityEngine;

public class OutOfBoundsRespawner : MonoBehaviour
{
    private float _topBoundry;
    private float _bottomBoundry;
    private float _leftBoundry;
    private float _rightBoundry;

    // Start is called before the first frame update
    void Start()
    {
        _topBoundry = Camera.main.orthographicSize;
        _bottomBoundry = -Camera.main.orthographicSize;
        _leftBoundry = -Camera.main.orthographicSize * Camera.main.aspect;
        _rightBoundry = Camera.main.orthographicSize * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        // Teleport the game object
        if (transform.position.x > _rightBoundry)
        {
            transform.position = new Vector3(_leftBoundry, transform.position.y, 0);
        }
        else if (transform.position.x < _leftBoundry)
        {
            transform.position = new Vector3(_rightBoundry, transform.position.y, 0);
        }
        else if (transform.position.y > _topBoundry)
        {
            transform.position = new Vector3(transform.position.x, _bottomBoundry, 0);
        }
        else if (transform.position.y < _bottomBoundry)
        {
            transform.position = new Vector3(transform.position.x, _topBoundry, 0);
        }
        else
        {
            return;
        }
    }
}
