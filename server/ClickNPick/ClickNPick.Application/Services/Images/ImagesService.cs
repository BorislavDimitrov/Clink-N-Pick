﻿
using ClickNPick.Application.Abstractions.Repositories;
using ClickNPick.Application.Abstractions.Services;
using ClickNPick.Application.Exceptions.General;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace ClickNPick.Application.Services.Images;

public class ImagesService : IImagesService
{
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IRepository<Domain.Models.Image> _imagesRepository;

    public ImagesService(ICloudinaryService cloudinaryService, IRepository<Domain.Models.Image> imagesRepository)
    {
        _cloudinaryService = cloudinaryService;
        _imagesRepository = imagesRepository;
    }

    public async Task<string> CreateImageAsync(IFormFile image, int resizeWidth, int resizeHeight)
    {
        var newImage = new Domain.Models.Image();

        using var imageLoad = await Image.LoadAsync(image.OpenReadStream());

        var byteArray = await ProcessImageAsync(imageLoad, resizeWidth, resizeHeight);

        var uploadResult = await _cloudinaryService.UploadPictureAsync(byteArray, Guid.NewGuid().ToString(), "ApplicationImages", resizeWidth, resizeHeight);

        newImage.Url = uploadResult.Item1;
        newImage.PublicId = uploadResult.Item2;

        await _imagesRepository.AddAsync(newImage);
        await _imagesRepository.SaveChangesAsync();

        return newImage.Id;
    }

    public async Task DeleteImageAsync(string imageId)
    {
        var imageToDelete = await _imagesRepository
            .All()
            .FirstOrDefaultAsync(x => x.Id == imageId);

        if (imageToDelete == null)
        {
            throw new NotFoundException($"Image with id {imageId} doesnt exist.");
        }

        await _cloudinaryService.DeleteImageAsync(imageToDelete.PublicId);
        _imagesRepository.SoftDelete(imageToDelete);

        await _imagesRepository.SaveChangesAsync();
    }

    public async Task<Domain.Models.Image> GetImageByIdAsync(string imageId)
    {
        return await _imagesRepository.All()
            .FirstOrDefaultAsync(x => x.Id == imageId);
    }

    public async Task<List<Domain.Models.Image>> GetProductNonThumbnailImages(string productId)
            => await _imagesRepository.All()
            .Where(x => x.IsThumbnail == false && x.ProductId == productId)
            .ToListAsync();

    public async Task<Domain.Models.Image> GetProductThumbnail(string productId)
            => await _imagesRepository.All()
            .FirstOrDefaultAsync(x => x.ProductId == productId && x.IsThumbnail == true);

    private async Task<byte[]> ProcessImageAsync(Image image, int resizeWidth, int resizeHeight)
    {
        image.Mutate(x => x.Resize(resizeWidth, resizeHeight));

        await using var ms = new MemoryStream();
        image.Save(ms, new JpegEncoder { Quality = 100 });

        return ms.ToArray();
    }
}
