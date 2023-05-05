using blazor_tailwind_airbnb.Models;

namespace blazor_tailwind_airbnb.Data;

public static class Images
{
    public static Base64Image DefaultHomeImage => new(FileToBase64("./wwwroot/images/default-image.jpg"));

    private static string FileToBase64(string path)
    {
        var bytes = File.ReadAllBytes(path);
        return Convert.ToBase64String(bytes);
    }
}