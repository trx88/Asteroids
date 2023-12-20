using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public class MainInstaller : MonoInstaller
{
    public override void InstallBindings()
    {

        InstallControllers();
        InstallServices();
        InstallSignals();
        InstallFactories();

        InstallRepositoryFactories();
        ConfigurePlayerPrefsRepositories();
    }

    private void InstallControllers()
    {
        Container.BindInterfacesAndSelfTo<AsteroidController>().AsSingle();
    }

    private void InstallServices()
    {
        Container.BindInterfacesAndSelfTo<AsteroidSpawnerService>().AsSingle();
        Container.BindInterfacesAndSelfTo<AsteroidHandlerService>().AsSingle();
    }

    private void InstallSignals()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<AsteroidCollisionSignal>();
    }

    private void InstallFactories()
    {
        Container.Bind<IAsteroidFactory>().To<AsteroidFactory>().AsSingle();
    }

    private void InstallRepositoryFactories()
    {
        Container.BindInterfacesAndSelfTo<PlayerPrefsRepositoryFactory>().AsSingle();
    }

    private void ConfigurePlayerPrefsRepositories()
    {
        var playerPrefRepoFactory = Container.Resolve<PlayerPrefsRepositoryFactory>();

        playerPrefRepoFactory.AddRepositoryConfig(
            new RepositoryConfig(typeof(HiScoreData),
            new InitializeHiScoreDataAction(playerPrefRepoFactory)
            ));
    }
}
