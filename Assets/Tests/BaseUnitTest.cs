using RovioAsteroids.Actions;
using RovioAsteroids.Controllers;
using RovioAsteroids.MonoBehaviors.GameObjectFactory;
using RovioAsteroids.MonoBehaviors.GameObjectFactory.Abstraction;
using RovioAsteroids.MVVM.ViewModels;
using RovioAsteroids.Repository.Items.DataModels;
using RovioAsteroids.Repository.Repositories.RepositoryFactories;
using RovioAsteroids.Services;
using RovioAsteroids.Signals;
using RovioAsteroids.Utils;
using Zenject;

namespace RovioAsteroids.Tests
{
    public class BaseUnitTest : ZenjectUnitTestFixture
    {
        protected virtual void Init()
        {
            InstallBindings();
        }

        public void InstallBindings()
        {
            InstallRepositoryFactories();
            InstallSignals();
            InstallUtils();
            InstallFactories();
            InstallServices();
            InstallControllers();
            InstallViewModels();

            //Done here, so GameController does not need to inject AsteroidController.
            Container.Resolve<EnemyCollisionController>();
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
            Container.BindInterfacesAndSelfTo<EnemyCollisionController>().AsSingle();
        }

        private void InstallServices()
        {
            Container.BindInterfacesAndSelfTo<EnemySpawnerService>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyHandlerService>().AsSingle();
            Container.BindInterfacesAndSelfTo<ScoringService>().AsSingle();
        }

        private void InstallSignals()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<EnemyCollisionSignal>();
        }

        private void InstallFactories()
        {
            Container.Bind<IEnemyFactory>().To<EnemyFactory>().AsSingle();
            Container.Bind<ILaserFactory>().To<LaserFactory>().AsSingle();
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
                new RepositoryConfig(typeof(EnemyData)
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
