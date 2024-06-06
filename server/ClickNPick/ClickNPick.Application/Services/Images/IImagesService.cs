using ClickNPick.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace ClickNPick.Application.Services.Images;

public interface IImagesService
{
    Task<string> CreateImageAsync(IFormFile formFile, int resizeWidth, int resizeHeight);

    Task<Image> GetImageByIdAsync(string imageId);

    Task DeleteImageAsync(string imageId);

    Task<Image> GetProductThumbnail(string productId);

    Task<List<Image>> GetProductNonThumbnailImages(string productId);
}
