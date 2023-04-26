using blazor_tailwind_airbnb.Services;
using blazor_tailwind_airbnb.StateContainers;
using Microsoft.AspNetCore.Components;

namespace blazor_tailwind_airbnb.Shared.Modals.Register;

public partial class RegisterModal : IDisposable
{
    [Inject]
    public RegisterModalState ModalState { get;set; } = null!;
    [Inject]
    public IAuthenticationService AuthenticationService { get;set; } = null!;

    private readonly RegisterModel dataModel = new();
    public bool IsOpen { get;set; }

    private bool isLoading;

    private async Task HandleValidSubmit()
    {
        try
        {
            isLoading = true;
            var result = await AuthenticationService.Register(
                                    dataModel.Email ?? string.Empty,
                                    dataModel.Name ?? string.Empty,
                                    dataModel.Password ?? string.Empty);
            if(result)
                ModalState.Property = StateContainers.IsOpen.False;
        }
        finally
        {
            isLoading = false;
        }
    }

    public void Dispose()
    {
        ModalState.OnChange -= OnOpenStateChange;
        GC.SuppressFinalize(this);
    }

    private void OnClose()
    {
        StateHasChanged();
        IsOpen = false;
    }

    protected override void OnInitialized()
    {
        ModalState.OnChange += OnOpenStateChange;
    }

    private void OnOpenStateChange()
    {
        IsOpen = ModalState.Property.Value;
        InvokeAsync(StateHasChanged);
    }
}