using System.Text.Json.Serialization;
using Microsoft.JSInterop;

namespace blazor_tailwind_airbnb.InteractiveMap;

public class Marker : InteractiveLayer
{
    [JsonIgnore]
    public LatLng LatLng { get; }
    [JsonIgnore]
    public MarkerOptions Options { get; }

    public Marker(LatLng latlng, MarkerOptions options)
    {
        LatLng = latlng;
        Options = options;
    }
}

public sealed class MarkerJSReference : InteractiveLayerJSReference<Marker>
{
    private MarkerJSReference(Marker value, JSEnvironment jSEnvironment, IJSObjectReference objectReference) : base(value, jSEnvironment, objectReference) { }

    public static async Task<MarkerJSReference> Create(Marker value, JSEnvironment jSEnvironment)
    {
        var reference = await jSEnvironment.JSRuntime.InvokeAsync<IJSObjectReference>("L.marker", value.LatLng, value.Options);

        return new MarkerJSReference(value, jSEnvironment, reference);
    }

    public async Task UpdateLatLng(LatLng newLatLng)
    {
        await JSEnvironment.LeafletModule.InvokeVoidAsync("LeafletMap.Marker.setLatLng", this.ObjectReference, newLatLng);
    }
}