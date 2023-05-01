using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace blazor_tailwind_airbnb.Shared.Modals.Rent.Category;

public partial class CategoryInput
{
    [Parameter]
    public string Id { get;set; } = "";
    [Parameter]
    public string? Label { get;set; }
    [Parameter]
    public RenderFragment<Data.CategoriesData.CategoryRenderArgs>? Icon { get;set; }
    [Parameter]
    public bool Selected { get;set; }
    [Parameter]
    public Action? OnClick { get;set; }
}