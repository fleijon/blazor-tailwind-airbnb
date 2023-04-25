namespace blazor_tailwind_airbnb.Services;

public interface IAuthenticationService
{
    event Action? LoginStatusChanged;
    bool IsLoggedIn { get; }
    void Login();
    void Logout();
    Task Register();
}

public class AuthneticationService : IAuthenticationService
{
    public bool IsLoggedIn { get;set; }

    public event Action? LoginStatusChanged;

    public void Login()
    {
        IsLoggedIn = true;

        LoginStatusChanged?.Invoke();
    }

    public void Logout()
    {
        IsLoggedIn = false;

        LoginStatusChanged?.Invoke();
    }

    public Task Register()
    {
        return Task.CompletedTask;
    }
}