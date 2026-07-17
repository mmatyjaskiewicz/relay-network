using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using SocialService.Application.DTOs.Requests;
using SocialService.Application.Interfaces;
using SocialService.Application.Settings;

namespace SocialService.Application.Storages;

public class MinioAvatarStorage(IMinioClient minioClient, IOptions<MinioSettings> options) : IAvatarStorage
{
    private readonly MinioSettings _settings = options.Value;
    
    public async Task <string> UploadAsync(UploadAvatarRequest request, CancellationToken cancellationToken = default)
    {
        var objectName = $"{Guid.NewGuid()}{Path.GetExtension(request.FileName)}";

        var putObjectArgs = new PutObjectArgs()
            .WithBucket(_settings.BucketName)
            .WithObject(objectName)
            .WithStreamData(request.Content)
            .WithObjectSize(request.Length)
            .WithContentType(request.ContentType);

        await minioClient.PutObjectAsync(putObjectArgs, cancellationToken);

        return objectName;
    }
}