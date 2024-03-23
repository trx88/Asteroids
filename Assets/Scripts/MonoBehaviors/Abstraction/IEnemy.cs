namespace RovioAsteroids.MonoBehaviors.Abstraction
{
    public interface IEnemy : IAddresableGameObject
    {
        public string AddressableKey
        {
            get;
        }
    }
}
