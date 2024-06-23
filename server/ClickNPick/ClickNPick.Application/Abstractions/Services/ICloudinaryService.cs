namespace ClickNPick.Application.Abstractions.Services;

public interface ICloudinaryService
{
    Task<(string,string)> UploadPictureAsync(byte[] data, string fileName, string folderName, int width, int height);
    Task DeleteImageAsync(string publicId);
}
