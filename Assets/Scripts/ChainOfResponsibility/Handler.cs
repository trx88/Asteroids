public interface IHandler<T>
{
    void SetSuccessor(IHandler<T> successor);
    void HandleItem(T item);
}

public abstract class Handler<T> : IHandler<T>
{
    protected IHandler<T>? Successor;

    public abstract void HandleItem(T item);

    public void SetSuccessor(IHandler<T> successor)
    {
        if (Successor != null)
        {
            throw new System.Exception("There is already a successor registered to this handler.");
        }

        Successor = successor;
    }
}

public class AsteroidSmallHandler : Handler<Asteroid>
{
    private readonly IAsteroidSpawnerService _asteroidSpawnerService;

    public AsteroidSmallHandler(IAsteroidSpawnerService asteroidSpawnerService)
    {
        _asteroidSpawnerService = asteroidSpawnerService;
    }

    public override void HandleItem(Asteroid item)
    {
        if (item.GetType() == typeof(AsteroidSmall))
        {
            _asteroidSpawnerService.DestroyAsteroid(item);
        }
        else
        {
            Successor?.HandleItem(item);
        }
    }
}

public class AsteroidLargeHandler : Handler<Asteroid>
{
    private readonly IAsteroidSpawnerService _asteroidSpawnerService;

    public AsteroidLargeHandler(IAsteroidSpawnerService asteroidSpawnerService)
    {
        _asteroidSpawnerService = asteroidSpawnerService;
    }

    public override void HandleItem(Asteroid item)
    {
        if (item.GetType() == typeof(AsteroidLarge))
        {
            _asteroidSpawnerService.DestroyAsteroid(item);
            for (int i = 0; i < 3; i++)
            {
                _asteroidSpawnerService.SpawnSmallFromLarge(item.transform.position);
            }
        }
        else
        {
            Successor?.HandleItem(item);
        }
    }
}
