using blazor_tailwind_airbnb.Models;

namespace blazor_tailwind_airbnb.Services;

public interface IListingsService
{
    Task AddListing(Listing listing);
    Task<IReadOnlyList<Listing>> GetListings();
    Task<Listing?> GetListing(ListingId listingId);
}

public class ListingService : IListingsService
{
    private readonly List<Listing> _listings = new();

    public ListingService()
    {
        var listing1 = new Listing()
        {
            ListingId = new ListingId(Guid.NewGuid()),
            Category = new("Modern"),
            Location = new("SE"),
            Info = new(100, 20, 20),
            Image = new(Data.Images.DefaultHomeImage.Content),
            Description = new("Cottage in Sweden", "A small cozy cottage located in the swedish forest."),
            Price = new(100)
        };

        _listings.Add(listing1);
    }

    public async Task AddListing(Listing listing)
    {
        _listings.Add(listing);
        await Task.Delay(1000);
    }

    public Task<Listing?> GetListing(ListingId listingId) =>
        Task.FromResult<Listing?>(_listings.Find(l => l.ListingId == listingId));

    public Task<IReadOnlyList<Listing>> GetListings()
    {
        return Task.FromResult<IReadOnlyList<Listing>>(_listings.AsReadOnly());
    }
}