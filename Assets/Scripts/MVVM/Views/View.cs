using RovioAsteroids.MVVM.ViewModels.Abstraction;
using RovioAsteroids.MVVM.Views.Abstraction;
using UnityEngine;
using Zenject;

namespace RovioAsteroids.MVVM.Views
{
    /// <summary>
    /// Skeleton view class.
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    public class View<TViewModel> : MonoBehaviour, IView where TViewModel : IViewModel
    {
        protected TViewModel ViewModel { get; private set; }

        /// <summary>
        /// Since ViewModel is injected here, all ViewModels should be installed in the Zenject installer.
        /// </summary>
        /// <param name="viewModel"></param>
        [Inject]
        private void Construct(TViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        protected virtual void SetupDataBindings()
        {
            //For base data binding. Good use case is localization.
        }

        protected virtual void SetupActionCallbacks()
        {
            //For interaction. Not needed now.
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
            //Not needed now.
        }

        public void Show()
        {
            //Not needed now, it's showing instantly and there's only one view.
        }
    }
}
