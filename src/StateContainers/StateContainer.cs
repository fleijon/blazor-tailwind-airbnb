namespace blazor_tailwind_airbnb.StateContainers;

public abstract class StateContainer<T>
{
    private T? _savedValue;

    public T? Property
    {
        get => _savedValue;
        set
        {
            _savedValue = value;
            NotifyStateChanged();
        }
    }

    public event Action? OnChange;

    private void NotifyStateChanged() => OnChange?.Invoke();
}