namespace blazor_tailwind_airbnb.StateContainers;

public abstract class StateContainer<T>
{
    private T? _savedValue;

    public T? Property
    {
        get => _savedValue;
        set
        {
            // TODO: Add the code below and make sure modals are toggled correctly after that

            /*
            if(_savedValue != null && _savedValue.Equals(value))
                return;
            */

            _savedValue = value;
            NotifyStateChanged();
        }
    }

    public event Action? OnChange;

    private void NotifyStateChanged() => OnChange?.Invoke();
}