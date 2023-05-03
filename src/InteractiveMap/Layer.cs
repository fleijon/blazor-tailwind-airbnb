using Microsoft.JSInterop;

namespace blazor_tailwind_airbnb.InteractiveMap;

public abstract class Layer {}

public abstract class LayerJSReference<T> : InteropObject<T> where T : Layer
{
    protected LayerJSReference(T value, JSEnvironment jSEnvironment, IJSObjectReference objectReference) : base(value, jSEnvironment, objectReference) {}

    public async Task<LayerJSReference<T>> AddTo(MapJSReference map)
    {
        await JSEnvironment.LeafletModule.InvokeVoidAsync("LeafletMap.Layer.addTo", ObjectReference, map.ObjectReference);
        return this;
    }

    public async Task<LayerJSReference<T>> Remove()
    {
        await JSEnvironment.LeafletModule.InvokeVoidAsync("LeafletMap.Layer.remove", ObjectReference);
        return this;
    }
}