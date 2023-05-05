using blazor_tailwind_airbnb.Models;
using blazor_tailwind_airbnb.Services;
using Microsoft.AspNetCore.Components;

namespace blazor_tailwind_airbnb.Pages;

public partial class Index
{
    [Inject]
    public IAuthenticationService AuthenticationService { get;set; } = null!;
    [Inject]
    public IListingsService ListingsService { get;set; } = null!;
    private bool emptyListings => Listings is null || Listings.Count == 0;
    private IReadOnlyList<Listing>? Listings;
    private User? CurrentUser;

    protected override async Task OnInitializedAsync()
    {
        CurrentUser = AuthenticationService.CurrentUser;
        Listings = await ListingsService.GetListings();
    }
}