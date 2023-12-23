namespace RovioAsteroids.MonoBehaviors.GameObjectFactory.Abstraction
{
    public interface IEnemyFactory
    {
        AsteroidLarge CreateLargeAsteroid();
        AsteroidSmall CreateSmallAsteroid();
        ShipEnemyController CreateEnemyShip();
    }
}
