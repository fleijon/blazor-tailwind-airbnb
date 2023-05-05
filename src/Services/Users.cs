using blazor_tailwind_airbnb.Models;

namespace blazor_tailwind_airbnb.Services;

public interface IUsers
{
    public Task<User?> AddUser(string username, string email);
    public Task<User?> GetUserByUsername(string username);
    public Task<User?> GetUserByEmail(string email);
    public Task<User?> GetUserById(UserId userId);
}

public class Users : IUsers
{
    public Users()
    {
        var defaultUser = new User(new UserId(Guid.NewGuid()), "test", "test");
        _users.Add(defaultUser);
    }

    public readonly List<User> _users = new();

    public Task<User?> AddUser(string username, string email)
    {
        if(_users.Any(u => u.Name == username || u.Email == email))
        {
            return Task.FromResult<User?>(null);
        }

        var userId = new UserId(Guid.NewGuid());
        var newUser = new User(userId, username, email);

        _users.Add(newUser);

        return Task.FromResult<User?>(newUser);
    }

    public Task<User?> GetUserByUsername(string username) =>
        Task.FromResult<User?>(_users.Find(u => u.Name == username));

    public Task<User?> GetUserByEmail(string email) =>
        Task.FromResult<User?>(_users.Find(u => u.Email == email));

    public Task<User?> GetUserById(UserId userId) =>
        Task.FromResult<User?>(_users.Find(u => u.Id == userId));
}