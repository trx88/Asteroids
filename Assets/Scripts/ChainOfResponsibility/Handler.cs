using RovioAsteroids.ChainOfResponsibility.Abstraction;
using RovioAsteroids.MonoBehaviors.Abstraction;

namespace RovioAsteroids.ChainOfResponsibility
{
    public abstract class Handler<T> : IHandler<T> where T : IEnemy
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
