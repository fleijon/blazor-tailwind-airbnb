namespace blazor_tailwind_airbnb.Services;

public interface IAuthenticationService
{
    event Action? LoginStatusChanged;
    bool IsLoggedIn { get; }
    void Login(string name, string password);
    void Logout();
    Task<bool> Register(string email, string user, string password);
}

public class AuthneticationService : IAuthenticationService
{
    public bool IsLoggedIn { get;set; }

    public event Action? LoginStatusChanged;

    public void Login(string name, string password)
    {
        IsLoggedIn = true;

        LoginStatusChanged?.Invoke();
    }

    public void Logout()
    {
        IsLoggedIn = false;

        LoginStatusChanged?.Invoke();
    }

    public async Task<bool> Register(string email, string user, string password)
    {
        await Task.Delay(2000);

        return true;
    }
}