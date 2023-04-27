using blazor_tailwind_airbnb.Icons;
using Microsoft.AspNetCore.Components;

namespace blazor_tailwind_airbnb.Data;

public static class CategoriesData
{
    public record Category(string Label, string Descrtiption , RenderFragment<CategoryRenderArgs> Icon);

    public readonly record struct CategoryRenderArgs(string CssClass);

    public static IEnumerable<Category> Categories =>
        new Category[]
        {
            new Category(
                "Beach",
                "Description",
                (arg) => (builder) =>
                {
                    builder.OpenComponent<BeachIcon>(0);
                    builder.AddAttribute(1, "class", arg.CssClass);
                    builder.CloseComponent();
                }
            ),
            new Category(
                "Windmill",
                "Description",
                (arg) => (builder) =>
                {
                    builder.OpenComponent<BeachIcon>(0);
                    builder.AddAttribute(1, "class", arg.CssClass);
                    builder.CloseComponent();
                }
            ),
            new Category(
                "Modern",
                "Description",
                (arg) => (builder) =>
                {
                    builder.OpenComponent<ModernIcon>(0);
                    builder.AddAttribute(1, "class", arg.CssClass);
                    builder.CloseComponent();
                }
            )
        };
}