namespace RovioAsteroids.ChainOfResponsibility.Abstraction
{
    public interface IHandler<T>
    {
        void SetSuccessor(IHandler<T> successor);
        void HandleItem(T item);
    }
}
