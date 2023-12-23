using UnityEngine;
using Zenject;

namespace RovioAsteroids.Utils
{
    /// <summary>
    /// "Teleports" the GameObject from one edge of the screen to another.
    /// </summary>
    public class OutOfBoundsRespawner : MonoBehaviour
    {
        private MapHelper _mapHelper;

        [Inject]
        private void Construct(MapHelper mapHelper)
        {
            _mapHelper = mapHelper;
        }

        void Update()
        {
            if (transform.position.x > _mapHelper.Right)
            {
                transform.position = new Vector3(_mapHelper.Left, transform.position.y, 0);
            }
            else if (transform.position.x < _mapHelper.Left)
            {
                transform.position = new Vector3(_mapHelper.Right, transform.position.y, 0);
            }
            else if (transform.position.y > _mapHelper.Top)
            {
                transform.position = new Vector3(transform.position.x, _mapHelper.Bottom, 0);
            }
            else if (transform.position.y < _mapHelper.Bottom)
            {
                transform.position = new Vector3(transform.position.x, _mapHelper.Top, 0);
            }
            else
            {
                return;
            }
        }
    }
}
