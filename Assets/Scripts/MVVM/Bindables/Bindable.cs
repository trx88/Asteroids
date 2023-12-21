using RovioAsteroids.MVVM.Bindables.Abstraction;
using System;

namespace RovioAsteroids.MVVM.Bindables
{
    public class Bindable<T> : IBindable<T>
    {
        private Action<T> _itemChanged;

        public void BindTo(Action<T> action)
        {
            if (action == null)
            {
                throw new Exception();
            }

            _itemChanged += action;
        }

        public void SetPropertyValue(T value)
        {
            _itemChanged?.Invoke(value);
        }
    }
}
