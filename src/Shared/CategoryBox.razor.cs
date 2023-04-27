using Microsoft.AspNetCore.Components;

namespace blazor_tailwind_airbnb.Shared;

public partial class CategoryBox
{
    [Inject]
    public NavigationManager NavigationManager { get;set; } = null!;
    [EditorRequired]
    [Parameter]
    public string Label { get;set; } = null!;
    [Parameter]
    public bool Selected { get;set; }
    [Parameter]
    public RenderFragment? Icon { get;set; }
    private void onSelect()
    {
        if(Selected)
        {
            // Deselect
            NavigationManager.NavigateTo("/");
        }
        else
        {
            var query = new Dictionary<string, object?>(){
                { "category", Label }
            };

            var url = NavigationManager.GetUriWithQueryParameters("/", query);

            NavigationManager.NavigateTo(url);
        }
    }
}