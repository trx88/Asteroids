namespace RovioAsteroids.MonoBehaviors.GameObjectFactory.Abstraction
{
    public interface IAsteroidFactory
    {
        AsteroidLarge CreateLargeAsteroid();
        AsteroidSmall CreateSmallAsteroid();
    }
}
