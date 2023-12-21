using System;

namespace RovioAsteroids.MVVM.Bindables.Abstraction
{
    public interface IBindable<out T>
    {
        void BindTo(Action<T> action);
    }
}
