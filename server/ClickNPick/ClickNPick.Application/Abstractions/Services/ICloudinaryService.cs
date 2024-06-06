using ClickNPick.Application.Common;

namespace ClickNPick.Application.Abstractions.Services;

public interface ICloudinaryService
{
    Task<CloudinaryUploadResult> UploadPictureAsync(byte[] data, string fileName, string folderName, int width, int height);
    Task DeleteImageAsync(string publicId);
}
