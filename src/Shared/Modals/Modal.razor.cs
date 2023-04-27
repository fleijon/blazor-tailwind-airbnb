using Microsoft.AspNetCore.Components;

namespace blazor_tailwind_airbnb.Shared.Modals;

public partial class Modal
{
    [Parameter]
    public string? Label { get;set; }
    [Parameter]
    public bool IsOpen { get;set; }
    [Parameter]
    public EventCallback<bool> IsOpenChanged { get; set; }
    [Parameter]
    public RenderFragment? ChildContent { get;set; }
    [Parameter]
    public bool Disabled { get;set; }
    private const int transitionTime = 300; // Not configurable at the moment

    private bool showModal;
    private bool previousIsOpen;

    protected override async Task OnParametersSetAsync()
    {
        if(IsOpen && IsOpen != previousIsOpen)
        {
            // TODO: Could a transition be managed without Task.Delay?
            await Task.Delay(1);
            showModal = true;
        }

        if(!IsOpen && IsOpen != previousIsOpen)
        {
            await HandleOnClose();
        }

        previousIsOpen = IsOpen;
    }

    private async Task HandleOnClose()
    {
        if(Disabled)
            return;

        showModal = false;
        await Task.Delay(transitionTime);
        IsOpen = false;

        await IsOpenChanged.InvokeAsync(IsOpen);
    }
}
