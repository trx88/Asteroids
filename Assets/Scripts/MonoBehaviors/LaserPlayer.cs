namespace RovioAsteroids.MonoBehaviors
{
    public class LaserPlayer : Laser
    {
        void Start()
        {
            Init();
            Destroy(gameObject, 1.5f);
        }

        protected override void Init()
        {
            base.Init();
        }
    }
}
