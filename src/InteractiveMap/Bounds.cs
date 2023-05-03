using Microsoft.JSInterop;

namespace blazor_tailwind_airbnb.InteractiveMap;

public record Bounds(Point Min, Point Max);

public sealed class BoundsJSReference : InteropObject<Bounds>
{
    private BoundsJSReference(Bounds value, IJSObjectReference objectReference, JSEnvironment module) : base(value, module, objectReference){}

    public static async Task<BoundsJSReference> Create(Bounds value, JSEnvironment jSEnvironment)
    {
        var min = await PointJSReference.Create(value.Min, jSEnvironment);
        var max = await PointJSReference.Create(value.Max, jSEnvironment);
        var reference = await jSEnvironment.JSRuntime.InvokeAsync<IJSObjectReference>("L.bounds", min.ObjectReference, max.ObjectReference);

        return new BoundsJSReference(value, reference, jSEnvironment);
    }

    public override string ToString()
    {
        return $"[{Value.Min}, {Value.Max}]";
    }
}