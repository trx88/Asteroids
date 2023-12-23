namespace RovioAsteroids.MonoBehaviors
{
    public class LaserEnemy : Laser
    {
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
