using Microsoft.AspNetCore.Components;

namespace blazor_tailwind_airbnb.Icons;

public abstract partial class IconBase : ComponentBase
{
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? InputAttributes { get; set; }
    [Parameter]
    public string Fill { get;set; } = "#FFFFFF"; // TODO: Remove Fill property, it is not used

    public abstract string SVGCode { get; }
}