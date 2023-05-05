using Microsoft.AspNetCore.Components;

namespace blazor_tailwind_airbnb.Shared.Modals.Rent;

public partial class StepTemplate<T> : ComponentBase where T : new()
{
   [Parameter]
    public Func<T, Task>? OnValidSubmit { get;set; }
    [EditorRequired]
    [Parameter]
    public string StepId { get;set; } = null!;

    [Parameter]
    public T DataModel { get;set; } = new();

    [Parameter]
    public RenderFragment? ChildContent { get;set; }
}
