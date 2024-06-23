using ClickNPick.Application.Abstractions.Services;
using ClickNPick.Application.Exceptions.General;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace ClickNPick.Infrastructure.Services;

public class CloudinaryService : ICloudinaryService
{
    private readonly Cloudinary _cloudinaryUtility;

    public CloudinaryService(
        Cloudinary cloudinaryUtility)
    {
        _cloudinaryUtility = cloudinaryUtility;
    }

    public async Task<(string, string)> UploadPictureAsync(byte[] data, string fileName, string folderName, int width, int height)
    {
        UploadResult uploadResult = null;

        await using var ms = new MemoryStream(data);
        var uploadParams = new ImageUploadParams
        {
            Folder = folderName,
            File = new FileDescription(fileName, ms),
            Format = "jpg",
            Overwrite = true,
            Transformation = new Transformation().Width(width).Height(height).Crop("scale"),
        };

        uploadResult = await _cloudinaryUtility.UploadAsync(uploadParams);

        if (uploadResult.Error != null)
        {
            throw new OperationFailedException("The uploading of image failed.");
        }

        return ( uploadResult?.SecureUrl.AbsoluteUri, uploadResult.PublicId );
    }

    public async Task DeleteImageAsync(string publicId)
    {
        var deleteResult = await _cloudinaryUtility.DeleteResourcesAsync(publicId);

        if (deleteResult.Error != null)
        {
            throw new OperationFailedException("The deleting of image failed.");
        }
    }
}
