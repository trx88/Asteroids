using RovioAsteroids.MonoBehaviors;

namespace RovioAsteroids.Services.Abstraction
{
    public interface IScoringService
    {
        void UpdateScore(Asteroid asteroid);
    }
}
