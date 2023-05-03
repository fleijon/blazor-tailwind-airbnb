using Microsoft.JSInterop;

namespace blazor_tailwind_airbnb.InteractiveMap;

public readonly record struct Map(string ElementId, MapOptions Options);

public sealed class MapJSReference : InteropObject<Map>
{
    public static async Task<MapJSReference> Create(Map map, JSEnvironment jSEnvironment)
    {
        var reference = await jSEnvironment.JSRuntime.InvokeAsync<IJSObjectReference>("L.map", map.ElementId, map.Options);

        return new MapJSReference(map, reference, jSEnvironment);
    }

    private MapJSReference(Map value, IJSObjectReference reference, JSEnvironment jSEnvironment) : base(value, jSEnvironment, reference) {}

    public async Task<LatLng> GetCenter()
    {
        return await ObjectReference.InvokeAsync<LatLng>("getCenter");
    }

    public async Task<LatLngBounds> GetBounds()
    {
        return await ObjectReference.InvokeAsync<LatLngBounds>("getBounds");
    }

    public async Task<int> GetZoom()
    {
        return await ObjectReference.InvokeAsync<int>("getZoom");
    }

    public async Task<int> GetMinZoom()
    {
        return await ObjectReference.InvokeAsync<int>("getMinZoom");
    }

    public async Task<int> GetMaxZoom()
    {
        return await ObjectReference.InvokeAsync<int>("getMaxZoom");
    }

    public async Task<int> GetBoundsZoom(LatLngBoundsJSReference bounds, bool inside = false, PointJSReference? padding = null)
    {
        return await ObjectReference.InvokeAsync<int>("getBoundsZoom", bounds.ObjectReference, inside, padding?.ObjectReference);
    }

    public async Task<Point> GetSize()
    {
        return await ObjectReference.InvokeAsync<Point>("getSize");
    }

    public async Task<Bounds> GetPixelBounds()
    {
        return await ObjectReference.InvokeAsync<Bounds>("getPixelBounds");
    }

    public async Task<Point> GetPixelOrigin()
    {
        return await ObjectReference.InvokeAsync<Point>("getPixelOrigin");
    }

    public async Task<Bounds> GetPixelWorldBounds(int? zoom = null)
    {
        return await ObjectReference.InvokeAsync<Bounds>("getPixelWorldBounds", zoom);
    }

    public async Task<MapJSReference> SetView(LatLngJSReference center, int zoom)
    {
        // TODO: does setView update the center object?
        await JSEnvironment.LeafletModule.InvokeVoidAsync("LeafletMap.Map.setView", ObjectReference, center.ObjectReference, zoom);

        return this;
    }
}