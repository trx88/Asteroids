using RovioAsteroids.ChainOfResponsibility.Abstraction;
using RovioAsteroids.MonoBehaviors;

namespace RovioAsteroids.Services.Abstraction
{
    public interface IHandlerService
    {
        void RegisterHandler(IHandler<Asteroid> handler);
        void Handle(Asteroid item);
    }
}
