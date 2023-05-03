using blazor_tailwind_airbnb.InteractiveMap;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace blazor_tailwind_airbnb.Shared.Modals.Rent.Location;

public partial class LocationStep
{
    [Inject]
    public IJSRuntime JSRuntime { get; set; } = null!;

    [Parameter]
    public Action<LocationModel>? OnValidSubmit { get;set; }

    [EditorRequired]
    [Parameter]
    public string StepId { get;set; } = null!;

    [Parameter]
    public LocationModel DataModel { get;set; } = new();

    private Map map;
    private TileLayer? tile;
    private const string mapId = "locationMap";
    protected override void OnParametersSet()
    {
        // TODO: Refactor

        if(DataModel.Location is null)
        {
            map = new Map(mapId, new MapOptions
            {
                Center = LatLng.Default,
                Zoom = 2
            });
        }
        else
        {
            var location = Data.CountriesData.Countries[DataModel.Location];
            map = new Map(mapId, new MapOptions
            {
                Center = new LatLng(location.Latitude, location.Longitude),
                Zoom = 4
            });
        }
        tile = new TileLayer(
            "https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png",
            new TileLayerOptions
            {
                Attribution = @"Map data &copy; <a href=""https://www.openstreetmap.org/"">OpenStreetMap</a> contributors, " +
                    @"<a href=""https://creativecommons.org/licenses/by-sa/2.0/"">CC-BY-SA</a>"
            }
        );
    }

    private async Task UpdateMap() {
        if(DataModel?.Location == null)
            return;

        var location = Data.CountriesData.Countries[DataModel.Location];

        var mapCentre = new LatLng(location.Latitude, location.Longitude);
        map = new Map(mapId, new MapOptions
        {
            Center = mapCentre,
            Zoom = 4
        });

        StateHasChanged();
    }
}