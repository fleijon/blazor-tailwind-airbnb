using blazor_tailwind_airbnb.Models;

namespace blazor_tailwind_airbnb.Services;

public interface IAuthenticationService
{
    User? CurrentUser { get; }
    event Action? LoginStatusChanged;
    bool IsLoggedIn { get; }
    Task<bool> Login(string name, string password);
    void Logout();
    Task<bool> Register(string email, string user, string password);
}

public class AuthenticationService : IAuthenticationService
{
    private readonly IUsers _users;

    public AuthenticationService(IUsers users)
    {
        _users = users;
    }

    public bool IsLoggedIn { get;set; }

    public User? CurrentUser { get; private set; }

    public event Action? LoginStatusChanged;

    public async Task<bool> Login(string name, string password)
    {
        var user = await _users.GetUserByUsername(name);

        if(user == null)
            return false;

        IsLoggedIn = true;
        CurrentUser = user;

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

    public async Task<bool> Register(string email, string username, string password)
    {
        var user = await _users.AddUser(username, email);
        if(user is null)
            return false;

        return await Login(username, password);
    }
}