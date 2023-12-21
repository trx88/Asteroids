using System;

public interface IBindable
{
    void BindTo(Action action);
}

public interface IBindable<out T>
{
    void BindTo(Action<T> action);
}

public class Bindable<T> : IBindable<T>
{
    private Action<T> _itemChanged;

    public void BindTo(Action<T> action)
    {
        if(action == null)
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
