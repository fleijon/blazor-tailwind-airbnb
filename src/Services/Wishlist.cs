using blazor_tailwind_airbnb.Models;

namespace blazor_tailwind_airbnb.Services;

public interface IWishlist
{
    public Task AddToWishlist(UserId userId, ListingId listingId);
    public Task RemoveFromWishlist(UserId userId, ListingId listingId);
    public Task<IReadOnlyList<ListingId>> GetUserWishlist(UserId userId);
}

public class Wishlist : IWishlist
{
    private readonly Dictionary<UserId, HashSet<ListingId>> _wishlist = new();

    public Task AddToWishlist(UserId userId, ListingId listingId)
    {
        var userListing = GetUserListing(userId);
        if(userListing.Contains(listingId))
            return Task.CompletedTask;

        userListing.Add(listingId);
        return Task.CompletedTask;
    }

    private HashSet<ListingId> GetUserListing(UserId userId)
    {
        if(!_wishlist.ContainsKey(userId))
        {
            var userList = new HashSet<ListingId>();
            _wishlist[userId] = userList;

            return userList;
        }

        return _wishlist[userId];
    }

    public Task RemoveFromWishlist(UserId userId, ListingId listingId)
    {
        var userListing = GetUserListing(userId);
        if(!userListing.Contains(listingId))
            return Task.CompletedTask;

        userListing.Remove(listingId);

        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<ListingId>> GetUserWishlist(UserId userId)
    {
        var userListing = GetUserListing(userId);

        return Task.FromResult<IReadOnlyList<ListingId>>(userListing.ToList().AsReadOnly());
    }
}
