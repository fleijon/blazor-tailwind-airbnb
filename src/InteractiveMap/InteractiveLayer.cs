using Microsoft.JSInterop;

namespace blazor_tailwind_airbnb.InteractiveMap;

public class InteractiveLayer : Layer {}

public abstract class InteractiveLayerJSReference<T> : LayerJSReference<T> where T : InteractiveLayer
{
    protected InteractiveLayerJSReference(T value, JSEnvironment jSEnvironment, IJSObjectReference objectReference) : base(value, jSEnvironment, objectReference)
    {
    }
}