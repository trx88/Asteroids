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
