using RovioAsteroids.Utils;

namespace RovioAsteroids.MonoBehaviors
{
    public class AsteroidSmall : Enemy
    {
        public new string AddressableKey => StaticStrings.Addressable_AsteroidLarge;

        // Start is called before the first frame update
        void Start()
        {
            Init();
        }
    }
}
