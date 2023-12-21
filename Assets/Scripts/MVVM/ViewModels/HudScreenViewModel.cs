using RovioAsteroids.MVVM.Bindables;
using RovioAsteroids.Repository.Items.DataModels;
using RovioAsteroids.Repository.Repositories.Abstraction;
using RovioAsteroids.Repository.Repositories.RepositoryFactories;
using System.Linq;

namespace RovioAsteroids.MVVM.ViewModels
{
    public class HudScreenViewModel : ViewModel
    {
        public Bindable<GameSessionData> SessionData { get; private set; } = new Bindable<GameSessionData>();
        public Bindable<HiScoreData> ScoreData { get; private set; } = new Bindable<HiScoreData>();

        private IRepository<GameSessionData> _gameSessionDataRepository;
        private IRepository<HiScoreData> _hiScoreDataRepository;

        public HudScreenViewModel(
            InMemoryRepositoryFactory inMemoryRepositoryFactory,
            PlayerPrefsRepositoryFactory playerPrefsRepositoryFactory
            )
        {
            _gameSessionDataRepository = inMemoryRepositoryFactory.RepositoryOf<GameSessionData>();
            _hiScoreDataRepository = playerPrefsRepositoryFactory.RepositoryOf<HiScoreData>();
        }

        #region Overrides
        public override void UpdateData()
        {
            base.UpdateData();

            GameSessionData gameSessionData = _gameSessionDataRepository.Get(x => true).Single();
            SetGameSessionData(gameSessionData);

            HiScoreData hiScoreData = _hiScoreDataRepository.Get(x => true).Single();
            SetHiScoreData(hiScoreData);
        }

        public override void SubscribeToDataChanges()
        {
            base.SubscribeToDataChanges();

            _gameSessionDataRepository.ItemChanged += OnGameSessionDataChanged;
            _hiScoreDataRepository.ItemChanged += OnHiScoreDataChanged;
        }

        public override void UnsubscribeFromDataChanges()
        {
            base.UnsubscribeFromDataChanges();

            _gameSessionDataRepository.ItemChanged -= OnGameSessionDataChanged;
            _hiScoreDataRepository.ItemChanged -= OnHiScoreDataChanged;
        }
        #endregion

        #region Repo callbacks
        private void OnGameSessionDataChanged(GameSessionData gameSessionData)
        {
            SetGameSessionData(gameSessionData);
        }

        private void OnHiScoreDataChanged(HiScoreData hiScoreData)
        {
            SetHiScoreData(hiScoreData);
        }
        #endregion

        #region Setters
        private void SetGameSessionData(GameSessionData gameSessionData)
        {
            SessionData.SetPropertyValue(gameSessionData);
        }

        private void SetHiScoreData(HiScoreData hiScoreData)
        {
            ScoreData.SetPropertyValue(hiScoreData);
        }
        #endregion
    }
}
