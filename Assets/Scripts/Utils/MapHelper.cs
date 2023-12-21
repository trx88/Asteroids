using UnityEngine;
using Zenject;

namespace RovioAsteroids.Utils
{
    public class MapHelper
    {
        readonly Camera _camera;

        public MapHelper(
            [Inject(Id = "MainCamera")]
            Camera camera)
        {
            _camera = camera;
        }

        public float Bottom
        {
            get { return -_camera.orthographicSize; }
        }

        public float Top
        {
            get { return _camera.orthographicSize; }
        }

        public float Left
        {
            get { return -(_camera.aspect * _camera.orthographicSize); }
        }

        public float Right
        {
            get { return (_camera.aspect * _camera.orthographicSize); }
        }
    }
}
