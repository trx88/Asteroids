namespace RovioAsteroids.MonoBehaviors.GameObjectFactories.Abstraction
{
    public interface IEnemyFactory
    {
        AsteroidLarge CreateLargeAsteroid();
        AsteroidSmall CreateSmallAsteroid();
        ShipEnemyController CreateEnemyShip();
        T CreateEnemy<T>();
    }
}
