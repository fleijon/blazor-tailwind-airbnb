using blazor_tailwind_airbnb.Services;
using blazor_tailwind_airbnb.StateContainers;
using Microsoft.AspNetCore.Components;

namespace blazor_tailwind_airbnb.Shared.Modals;

public class RegisterModal : Modal, IDisposable
{
    [Inject]
    public RegisterModalState ModalState { get;set; } = null!;
    [Inject]
    public IAuthenticationService AuthenticationService { get;set; } = null!;

    protected override void OnInitialized()
    {
        ModalState.OnChange += OnOpenStateChange;
        OnClose = OnClose_Private;
        OnSubmit = OnRegister;
        Label = "Register";
        ActionLabel = "Submit";
        Body = (builder) =>
        {
            builder.OpenComponent<RegisterModalBody>(0);
            builder.AddAttribute(1, nameof(RegisterModalBody.Email), Email);
            builder.AddAttribute(2, nameof(RegisterModalBody.EmailChanged), EventCallback.Factory.Create<string?>(this, value => Email = value));
            builder.AddAttribute(3, nameof(RegisterModalBody.Name), User);
            builder.AddAttribute(4, nameof(RegisterModalBody.EmailChanged), EventCallback.Factory.Create<string?>(this, value => User = value));
            builder.AddAttribute(5, nameof(RegisterModalBody.Password), Password);
            builder.AddAttribute(6, nameof(RegisterModalBody.EmailChanged), EventCallback.Factory.Create<string?>(this, value => Password = value));
            builder.CloseComponent();
        };
    }


    private string? Email { get;set; }
    private string? User { get;set; }
    private string? Password { get;set; }

    private void OnOpenStateChange()
    {
        IsOpen = ModalState.Property.Value;
        InvokeAsync(StateHasChanged);
    }

    private async Task OnRegister()
    {
        await AuthenticationService.Register();
    }

    private void OnClose_Private()
    {
        ModalState.Property = StateContainers.IsOpen.False;
    }

    public void Dispose()
    {
        ModalState.OnChange -= OnOpenStateChange;
        GC.SuppressFinalize(this);
    }
}