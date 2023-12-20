using System.Collections.Generic;
using System.Linq;

public interface IHandlerService
{
    void RegisterHandler(IHandler<Asteroid> handler);
    void Handle(Asteroid item);
}

public class AsteroidHandlerService : IHandlerService
{
    private readonly List<IHandler<Asteroid>> _asteroidHandlers = new List<IHandler<Asteroid>>();

    private AsteroidHandlerService(IAsteroidSpawnerService asteroidSpawnerService)
    {
        RegisterHandler(new AsteroidSmallHandler(asteroidSpawnerService));
        RegisterHandler(new AsteroidLargeHandler(asteroidSpawnerService));
    }

    public void Handle(Asteroid item)
    {
        _asteroidHandlers.First().HandleItem(item);
    }

    public void RegisterHandler(IHandler<Asteroid> handler)
    {
        if (_asteroidHandlers.Count > 0)
        {
            _asteroidHandlers[_asteroidHandlers.Count - 1].SetSuccessor(handler);
        }

        _asteroidHandlers.Add(handler);
    }
}
