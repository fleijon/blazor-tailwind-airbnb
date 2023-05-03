using Microsoft.JSInterop;

namespace blazor_tailwind_airbnb.InteractiveMap;

public sealed class JSEnvironment : IAsyncDisposable
{
    private const string JsModulePath = "./js/leaflet.js";
    public IJSObjectReference LeafletModule { get; }
    public IJSRuntime JSRuntime { get; }

    private JSEnvironment(IJSRuntime jSRuntime, IJSObjectReference leafletModule)
    {
        JSRuntime = jSRuntime;
        LeafletModule  = leafletModule ?? throw new ArgumentNullException(nameof(leafletModule));
    }

    public static async Task<JSEnvironment> Create(IJSRuntime jsRuntime)
    {
        var reference = await jsRuntime.InvokeAsync<IJSObjectReference>("import", JsModulePath);

        return new JSEnvironment(jsRuntime, reference);
    }

    public async ValueTask DisposeAsync()
    {
        if(LeafletModule != null)
        {
            await LeafletModule.DisposeAsync();
        }

        GC.SuppressFinalize(this);
    }
}
