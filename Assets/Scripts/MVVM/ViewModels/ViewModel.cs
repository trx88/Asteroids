using RovioAsteroids.MVVM.ViewModels.Abstraction;

namespace RovioAsteroids.MVVM.ViewModels
{
    /// <summary>
    /// Base ViewModel class.
    /// </summary>
    public abstract class ViewModel : IViewModel
    {
        /// <summary>
        /// Subscribe to repository changes here.
        /// </summary>
        public virtual void SubscribeToDataChanges()
        {

        }

        /// <summary>
        /// Unsubscribe from repository changes here.
        /// </summary>
        public virtual void UnsubscribeFromDataChanges()
        {

        }

        /// <summary>
        /// Get/update data here.
        /// </summary>
        public virtual void UpdateData()
        {

        }
    }
}
