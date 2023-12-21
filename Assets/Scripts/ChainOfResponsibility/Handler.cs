using RovioAsteroids.ChainOfResponsibility.Abstraction;

namespace RovioAsteroids.ChainOfResponsibility
{
    public abstract class Handler<T> : IHandler<T>
    {
        protected IHandler<T>? Successor;

        public abstract void HandleItem(T item);

        public void SetSuccessor(IHandler<T> successor)
        {
            if (Successor != null)
            {
                throw new System.Exception("There is already a successor registered to this handler.");
            }

            Successor = successor;
        }
    }
}
