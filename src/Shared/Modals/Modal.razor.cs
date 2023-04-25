using Microsoft.AspNetCore.Components;

namespace blazor_tailwind_airbnb.Shared.Modals;

public abstract partial class Modal
{
    protected string? Label { get;set; }
    protected Action? OnClose { get;set; }
    protected Func<Task>? OnSubmit { get;set; }
    protected RenderFragment? Body { get;set; }
    protected RenderFragment? Footer { get;set; }
    protected bool Disabled { get;set; } = false;
    protected Action? SecondaryAction { get;set; }
    protected string? SecondaryLabel { get;set; }
    private bool isOpen;
    private const int transitionTime = 300; // Not configurable at the moment
    protected bool IsOpen
    {
        get => isOpen;
        set
        {
            if(isOpen == value)
                return;

            isOpen = value;
            if(isOpen)
            {
                Task.Run(ShowModal);
            }
        }
    }
    protected string? ActionLabel { get;set; }

    private bool showModal;

    private async Task ShowModal()
    {
        await InvokeAsync(StateHasChanged);
        showModal = true;
        await InvokeAsync(StateHasChanged);
    }

    private async Task HandleOnClose()
    {
        if(Disabled)
            return;

        showModal = false;
        await Task.Delay(transitionTime);

        OnClose?.Invoke();
    }

    private async Task HandleOnSubmit()
    {
        if(Disabled)
            return;

        await Task.Delay(transitionTime);

        OnSubmit?.Invoke();
    }
}
