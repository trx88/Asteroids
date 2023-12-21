namespace RovioAsteroids.MVVM.ViewModels.Abstraction
{
    public interface IViewModel
    {
        void UpdateData();

        void SubscribeToDataChanges();

        void UnsubscribeFromDataChanges();
    }
}
