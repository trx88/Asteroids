using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RovioAsteroids.MonoBehaviors
{
    public class LaserEnemy : Laser
    {
        // Start is called before the first frame update
        void Start()
        {
            Init();
            Destroy(gameObject, 1f);
        }

        protected override void Init()
        {
            base.Init();
        }
    }
}
