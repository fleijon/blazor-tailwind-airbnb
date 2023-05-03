using Microsoft.JSInterop;

namespace blazor_tailwind_airbnb.InteractiveMap;

public readonly record struct LatLng(double Lat, double Lng)
{
    public static LatLng Default => new(-42, 175);
}

public sealed class LatLngJSReference : InteropObject<LatLng>
{
    private LatLngJSReference(LatLng value, JSEnvironment jSEnvironment, IJSObjectReference reference) : base(value, jSEnvironment, reference) {}

    public static async Task<LatLngJSReference> Create(LatLng value, JSEnvironment jSEnvironment)
    {
        var reference = await jSEnvironment.JSRuntime.InvokeAsync<IJSObjectReference>("L.latLng", value.Lat, value.Lng);

        return new LatLngJSReference(value, jSEnvironment, reference);
    }

    public override string ToString()
    {
        return $"({Value.Lat}, {Value.Lng})";
    }
}