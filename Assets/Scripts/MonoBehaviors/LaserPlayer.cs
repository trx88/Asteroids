namespace RovioAsteroids.MonoBehaviors
{
    public class LaserPlayer : Laser
    {
        // Start is called before the first frame update
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
