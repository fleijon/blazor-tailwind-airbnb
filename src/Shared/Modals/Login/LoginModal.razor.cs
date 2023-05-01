using blazor_tailwind_airbnb.Services;
using blazor_tailwind_airbnb.StateContainers;
using Microsoft.AspNetCore.Components;

namespace blazor_tailwind_airbnb.Shared.Modals.Login;

public partial class LoginModal : IDisposable
{
    [Inject]
    public RegisterModalState RegisterModalState { get;set; } = null!;
    [Inject]
    public LoginModalState LoginModalState { get;set; } = null!;
    [Inject]
    public IAuthenticationService AuthenticationService { get;set; } = null!;

    private readonly LoginModel dataModel = new();
    public bool IsOpen { get;set; }

    private bool isLoading;

    private async Task HandleValidSubmit()
    {
        try
        {
            isLoading = true;
            var result = await AuthenticationService.Login(
                                    dataModel.Name ?? string.Empty,
                                    dataModel.Password ?? string.Empty);
            if(result)
                LoginModalState.Property = StateContainers.IsOpen.False;
        }
        finally
        {
            isLoading = false;
        }
    }

    private void GoToRegister()
    {
        LoginModalState.Property = StateContainers.IsOpen.False;
        RegisterModalState.Property = StateContainers.IsOpen.True;
    }

    public void Dispose()
    {
        RegisterModalState.OnChange -= OnOpenRegisterModalStateChange;
        LoginModalState.OnChange -= OnOpenLoginModalStateChange;
        GC.SuppressFinalize(this);
    }

    private void OnOpenLoginModalStateChange()
    {
        IsOpen = LoginModalState.Property.Value;
        InvokeAsync(StateHasChanged);
    }

    private void OnClose()
    {
        StateHasChanged();
        IsOpen = false;
    }

    protected override void OnInitialized()
    {
        RegisterModalState.OnChange += OnOpenRegisterModalStateChange;
        LoginModalState.OnChange += OnOpenLoginModalStateChange;
    }

    private void OnOpenRegisterModalStateChange()
    {
    }
}