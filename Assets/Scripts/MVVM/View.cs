using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public interface IView
{
    void Show();
    void Hide();
}

public class View<TViewModel> : MonoBehaviour, IView where TViewModel : IViewModel
{
    protected TViewModel ViewModel { get; private set; }

    [Inject]
    private void Construct(TViewModel viewModel)
    {
        ViewModel = viewModel;
    }

    protected virtual void SetupDataBindings()
    {

    }

    protected virtual void SetupActionCallbacks()
    {
        //For interaction
    }

    private void OnEnable()
    {
        SetupDataBindings();
        ViewModel.UpdateData();
        ViewModel.SubscribeToDataChanges();
    }

    private void OnDisable()
    {
        ViewModel.UnsubscribeFromDataChanges();
    }

    public void Hide()
    {
        
    }

    public void Show()
    {
        
    }
}

public class HudScreenView : View<HudScreenViewModel>, IView
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
        _scoreText.text = gameSessionData.Score.ToString();
        _livesText.text = gameSessionData.Lives.ToString();
        _waveText.text = gameSessionData.Wave.ToString();
    }

    private void OnHiScoreDataChanged(HiScoreData hiScoreData)
    {
        _hiscoreText.text = hiScoreData.HiScore.ToString();
    }
}
