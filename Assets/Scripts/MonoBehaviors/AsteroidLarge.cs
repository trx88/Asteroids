using RovioAsteroids.Utils;

namespace RovioAsteroids.MonoBehaviors
{
    public class AsteroidLarge : Enemy
    {
        public new string AddressableKey => StaticStrings.Addressable_AsteroidLarge;

        void Start()
        {
            Init();
        }
    }
}
