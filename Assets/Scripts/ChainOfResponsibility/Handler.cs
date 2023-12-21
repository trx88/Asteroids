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
    private readonly IScoringService _scoringService;

    public AsteroidSmallHandler(
        IAsteroidSpawnerService asteroidSpawnerService,
        IScoringService scoringService
        )
    {
        _asteroidSpawnerService = asteroidSpawnerService;
        _scoringService = scoringService;
    }

    public override void HandleItem(Asteroid item)
    {
        if (item.GetType() == typeof(AsteroidSmall))
        {
            _scoringService.UpdateScore(item);
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
    private readonly IScoringService _scoringService;

    public AsteroidLargeHandler(
        IAsteroidSpawnerService asteroidSpawnerService,
        IScoringService scoringService
        )
    {
        _asteroidSpawnerService = asteroidSpawnerService;
        _scoringService = scoringService;
    }

    public override void HandleItem(Asteroid item)
    {
        if (item.GetType() == typeof(AsteroidLarge))
        {
            _scoringService.UpdateScore(item);
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
