namespace blazor_tailwind_airbnb.Services;

public interface IAuthenticationService
{
    string? CurrentUser { get; }
    event Action? LoginStatusChanged;
    bool IsLoggedIn { get; }
    Task<bool> Login(string name, string password);
    void Logout();
    Task<bool> Register(string email, string user, string password);
}

public class AuthneticationService : IAuthenticationService
{
    public bool IsLoggedIn { get;set; }

    public string? CurrentUser { get; private set; }

    public event Action? LoginStatusChanged;

    public async Task<bool> Login(string name, string password)
    {
        IsLoggedIn = true;
        CurrentUser = name;

        await Task.Delay(1000);

        LoginStatusChanged?.Invoke();

        return true;
    }

    public void Logout()
    {
        CurrentUser = null;
        IsLoggedIn = false;

        LoginStatusChanged?.Invoke();
    }

    public async Task<bool> Register(string email, string user, string password)
    {
        await Task.Delay(2000);

        return true;
    }
}