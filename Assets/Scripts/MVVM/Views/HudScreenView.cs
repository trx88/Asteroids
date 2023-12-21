using RovioAsteroids.MVVM.ViewModels;
using RovioAsteroids.Repository.Items.DataModels;
using TMPro;
using UnityEngine;

namespace RovioAsteroids.MVVM.Views
{
    public class HudScreenView : View<HudScreenViewModel>
    {
        [SerializeField] private TextMeshProUGUI _scoreText = default;
        [SerializeField] private TextMeshProUGUI _livesText = default;
        [SerializeField] private TextMeshProUGUI _waveText = default;
        [SerializeField] private TextMeshProUGUI _hiscoreText = default;

        protected override void SetupDataBindings()
        {
            base.SetupDataBindings();

            ViewModel.SessionData.BindTo(OnGameSessionDataChanged);
            ViewModel.ScoreData.BindTo(OnHiScoreDataChanged);
        }

        private void OnGameSessionDataChanged(GameSessionData gameSessionData)
        {
            _scoreText.text = $"Score: {gameSessionData.Score}";
            _livesText.text = $"Lives: {gameSessionData.Lives}";
            _waveText.text = $"Wave: {gameSessionData.Wave}";
        }

        private void OnHiScoreDataChanged(HiScoreData hiScoreData)
        {
            _hiscoreText.text = $"HiScore: {hiScoreData.HiScore}";
        }
    }
}
