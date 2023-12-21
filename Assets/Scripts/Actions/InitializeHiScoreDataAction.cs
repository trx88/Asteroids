public abstract class InitializeAction
{
    public abstract void Invoke();
}


public class InitializeHiScoreDataAction : InitializeAction
{
    private readonly PlayerPrefsRepositoryFactory _playerPrefsRepositoryFactory;

    public InitializeHiScoreDataAction(PlayerPrefsRepositoryFactory repositoryFactory)
    {
        _playerPrefsRepositoryFactory = repositoryFactory;
    }

    public override void Invoke()
    {
        _playerPrefsRepositoryFactory.RepositoryOf<HiScoreData>().Create(
            new HiScoreData { HiScore = 999 }
            );
    }
}

public class InitializeGameSessionDataAction : InitializeAction
{
    private readonly InMemoryRepositoryFactory _inMemoryRepositoryFactory;

    public InitializeGameSessionDataAction(InMemoryRepositoryFactory repositoryFactory)
    {
        _inMemoryRepositoryFactory = repositoryFactory;
    }

    public override void Invoke()
    {
        _inMemoryRepositoryFactory.RepositoryOf<GameSessionData>().Create(
            new GameSessionData
            {
                Lives = 3,
                Score = 0,
                Wave = 1
            });
    }
}
