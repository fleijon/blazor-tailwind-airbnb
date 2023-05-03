using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace blazor_tailwind_airbnb.InteractiveMap;

/// <summary>
///     Source: https://github.com/darnton/LeafletBlazor/
/// </summary>
public partial class LeafletMap : ComponentBase
{
    [Inject]
    public IJSRuntime JSRuntime { get; set; }

    [EditorRequired]
    [Parameter]
    public Map Map { get; set; }

    [EditorRequired]
    [Parameter]
    public TileLayer TileLayer { get; set; } = null!;

    private MapJSReference? PositionMap;
    private TileLayerJSReference? OpenStreetMapsTileLayer;
    private Map currentVisibleMap;
    private JSEnvironment? jsEnv;
    private MarkerJSReference? currentVisibleMarker;

    protected async override Task OnAfterRenderAsync(bool firstRender)
    {
        jsEnv ??= await JSEnvironment.Create(JSRuntime);

        if (firstRender)
        {
            PositionMap = await MapJSReference.Create(Map, jsEnv);
            OpenStreetMapsTileLayer = await TileLayerJSReference.Create(TileLayer, jsEnv);

            await OpenStreetMapsTileLayer.AddTo(PositionMap);
        }

        if(currentVisibleMap != Map)
        {
            // Set new center when the map has updated
            currentVisibleMap = Map;
            var newPos = Map.Options.Center;
            var latlng = await LatLngJSReference.Create(newPos, jsEnv);

            if(PositionMap is null)
                return;

            await PositionMap.SetView(latlng, Map.Options.Zoom);

            if(newPos != LatLng.Default)
            {
                if(currentVisibleMarker is null)
                {
                    currentVisibleMarker = await AddMarker(jsEnv, PositionMap, latlng.Value);
                }
                else
                {
                    await currentVisibleMarker.UpdateLatLng(latlng.Value);
                }
            }

            StateHasChanged();
        }
    }

    private static async Task<MarkerJSReference> AddMarker(JSEnvironment jsEnv, MapJSReference map, LatLng center)
    {
        var marker = new Marker(center, new MarkerOptions()
        {
            Keyboard = true,
            ZIndexOffset = 0,
            Opacity = 1.0f,
            RiseOnHover = true,
            RiseOffset = 250,
        });
        var markerRef = await MarkerJSReference.Create(marker, jsEnv);
        await markerRef.AddTo(map);

        return markerRef;
    }
}