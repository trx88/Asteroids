using RovioAsteroids.MVVM.ViewModels.Abstraction;

namespace RovioAsteroids.MVVM.ViewModels
{
    public abstract class ViewModel : IViewModel
    {
        public virtual void SubscribeToDataChanges()
        {

        }

        public virtual void UnsubscribeFromDataChanges()
        {

        }

        public virtual void UpdateData()
        {

        }
    }
}
