using RovioAsteroids.Repository.Items.DataModels;
using RovioAsteroids.Services;
using RovioAsteroids.Signals;
using RovioAsteroids.Utils;
using Zenject;

namespace RovioAsteroids.Installers
{
    public class MainInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallRepositoryFactories();
            InstallSignals();
            InstallUtils();
            InstallFactories();
            InstallServices();
            InstallControllers();
            InstallViewModels();

            //Done here, so GameController does not need to inject AsteroidController.
            Container.Resolve<AsteroidController>();
        }

        private void InstallUtils()
        {
            Container.Bind<MapHelper>().AsSingle();
        }

        private void InstallViewModels()
        {
            Container.BindInterfacesAndSelfTo<HudScreenViewModel>().AsSingle();
        }

        private void InstallControllers()
        {
            Container.BindInterfacesAndSelfTo<AsteroidController>().AsSingle();
        }

        private void InstallServices()
        {
            Container.BindInterfacesAndSelfTo<AsteroidSpawnerService>().AsSingle();
            Container.BindInterfacesAndSelfTo<AsteroidHandlerService>().AsSingle();
            Container.BindInterfacesAndSelfTo<ScoringService>().AsSingle();
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
            Container.BindInterfacesAndSelfTo<InMemoryRepositoryFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerPrefsRepositoryFactory>().AsSingle();

            ConfigureInMemoryRepositories();
            ConfigurePlayerPrefsRepositories();
        }

        private void ConfigureInMemoryRepositories()
        {
            var inMemoryRepoFactory = Container.Resolve<InMemoryRepositoryFactory>();

            inMemoryRepoFactory.AddRepositoryConfig(
                new RepositoryConfig(typeof(GameSessionData),
                new InitializeGameSessionDataAction(inMemoryRepoFactory)
                ));
            inMemoryRepoFactory.AddRepositoryConfig(
                new RepositoryConfig(typeof(AsteroidData)
                ));
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
}
