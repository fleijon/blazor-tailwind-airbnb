using Microsoft.JSInterop;

namespace blazor_tailwind_airbnb.InteractiveMap;

public record struct Point(double X, double Y, bool Round = false);

public sealed class PointJSReference : InteropObject<Point>
{
    private PointJSReference(JSEnvironment jSEnvironment, IJSObjectReference reference, Point value) : base(value, jSEnvironment, reference){}

    public override string ToString()
    {
        return $"({Value.X}, {Value.Y})";
    }

    public static async Task<PointJSReference> Create(Point value, JSEnvironment jSEnvironment)
    {
        var reference = await jSEnvironment.JSRuntime.InvokeAsync<IJSObjectReference>("L.point", value.X, value.Y, value.Round);

        return new PointJSReference(jSEnvironment, reference, value);
    }
}