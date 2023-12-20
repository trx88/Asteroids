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
