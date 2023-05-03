using Microsoft.JSInterop;

namespace blazor_tailwind_airbnb.InteractiveMap;

public abstract class InteropObject<T> : IAsyncDisposable
{
    public T Value { get; }
    internal JSEnvironment JSEnvironment { get;}
    internal IJSObjectReference ObjectReference { get;}
    protected InteropObject(T value, JSEnvironment jSEnvironment, IJSObjectReference objectReference)
    {
        Value = value;
        JSEnvironment = jSEnvironment;
        ObjectReference = objectReference;
    }

    public async ValueTask DisposeAsync()
    {
        if (ObjectReference != null)
            await ObjectReference.DisposeAsync();

        GC.SuppressFinalize(this);
    }
}