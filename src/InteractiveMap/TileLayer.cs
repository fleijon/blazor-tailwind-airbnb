using System.Text.Json.Serialization;
using Microsoft.JSInterop;

namespace blazor_tailwind_airbnb.InteractiveMap;

public class TileLayer : Layer
{
    public TileLayer(string urlTemplate, TileLayerOptions options)
    {
        UrlTemplate = urlTemplate;
        Options = options;
    }
    [JsonIgnore] public string UrlTemplate { get; }
    [JsonIgnore] public TileLayerOptions Options { get; }

}

public sealed class TileLayerJSReference : LayerJSReference<TileLayer>
{
    private TileLayerJSReference(TileLayer value, JSEnvironment jSEnvironment, IJSObjectReference reference) : base(value, jSEnvironment, reference){}

    public static async Task<TileLayerJSReference> Create(TileLayer value, JSEnvironment jSEnvironment)
    {
        var reference = await jSEnvironment.JSRuntime.InvokeAsync<IJSObjectReference>("L.tileLayer", value.UrlTemplate, value.Options);

        return new TileLayerJSReference(value, jSEnvironment, reference);
    }
}