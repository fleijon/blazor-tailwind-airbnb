using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.WebUtilities;
using static blazor_tailwind_airbnb.Data.CategoriesData;

namespace blazor_tailwind_airbnb.Shared.Navbar;

public partial class Categories
{
    [Inject]
    public NavigationManager NavigationManager { get;set; } = null!;
    private string? currentCategory;
    public bool isOnMainPage;

    protected override void OnInitialized()
    {
        NavigationManager.LocationChanged += OnLocationChanged;
    }

    private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
        isOnMainPage = uri.LocalPath == "/";

        if(QueryHelpers.ParseQuery(uri.Query).TryGetValue("category", out var token))
        {
            currentCategory = token[0];
        }

        StateHasChanged();
    }
}