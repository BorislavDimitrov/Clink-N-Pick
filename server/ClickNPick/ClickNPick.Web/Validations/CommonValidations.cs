using Microsoft.AspNetCore.Http;

namespace ClickNPick.Web.Validations;

public static class CommonValidations
{
    public static int MaxImagesCount = 10;
    public static string[] PermittedExtensions = { ".jpg", ".jpeg", ".png" };
    public static int MaxFileSize = 2 * 1024 * 1024;

    public static bool BeAValidImage(IFormFile file)
    {
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        return PermittedExtensions.Contains(extension);
    }

    public static bool BeWithinFileSizeLimit(IFormFile file)
     => file.Length <= MaxFileSize;
}
