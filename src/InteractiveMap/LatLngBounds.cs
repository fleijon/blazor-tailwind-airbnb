using Microsoft.JSInterop;

namespace blazor_tailwind_airbnb.InteractiveMap;

public readonly record struct LatLngBounds(LatLng Corner1, LatLng Corner2);

public sealed class LatLngBoundsJSReference : InteropObject<LatLngBounds>
{
    private LatLngBoundsJSReference(LatLngBounds value, JSEnvironment jSEnvironment, IJSObjectReference objectReference) : base(value, jSEnvironment, objectReference){}

    public static async Task<LatLngBoundsJSReference> Create(LatLngBounds value, JSEnvironment jSEnvironment)
    {
        var corner1 = await LatLngJSReference.Create(value.Corner1, jSEnvironment);
        var corner2 = await LatLngJSReference.Create(value.Corner2, jSEnvironment);
        var reference = await jSEnvironment.JSRuntime.InvokeAsync<IJSObjectReference>("L.latLngBounds", corner1.ObjectReference, corner2.ObjectReference);

        return new LatLngBoundsJSReference(value, jSEnvironment, reference);
    }
}