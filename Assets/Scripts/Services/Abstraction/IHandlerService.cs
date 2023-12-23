using RovioAsteroids.ChainOfResponsibility.Abstraction;
using RovioAsteroids.MonoBehaviors;

namespace RovioAsteroids.Services.Abstraction
{
    public interface IHandlerService
    {
        void RegisterHandler(IHandler<Enemy> handler);
        void Handle(Enemy item);
    }
}
