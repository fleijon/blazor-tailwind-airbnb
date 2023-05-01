using blazor_tailwind_airbnb.Services;
using blazor_tailwind_airbnb.StateContainers;
using Microsoft.AspNetCore.Components;

namespace blazor_tailwind_airbnb.Shared.Modals.Register;

public partial class RegisterModal : IDisposable
{
    [Inject]
    public RegisterModalState RegisterModalState { get;set; } = null!;
    [Inject]
    public LoginModalState LoginModalState { get;set; } = null!;
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
                                    dataModel.Email!,
                                    dataModel.Name!,
                                    dataModel.Password!);
            if(result)
            {
                await AuthenticationService.Login(dataModel.Name!, dataModel.Password!);
                RegisterModalState.Close();
            }
        }
        finally
        {
            isLoading = false;
        }
    }

    private void GoToLogin()
    {
        RegisterModalState.Close();
        LoginModalState.Open();
    }

    public void Dispose()
    {
        RegisterModalState.OnChange -= OnOpenStateChange;
        GC.SuppressFinalize(this);
    }

    private void OnClose()
    {
        StateHasChanged();
        IsOpen = false;
    }

    protected override void OnInitialized()
    {
        RegisterModalState.OnChange += OnOpenStateChange;
    }

    private void OnOpenStateChange()
    {
        IsOpen = RegisterModalState.Property.Value;
        InvokeAsync(StateHasChanged);
    }
}