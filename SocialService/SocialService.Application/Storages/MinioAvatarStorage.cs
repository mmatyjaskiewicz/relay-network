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
    
    public async Task<string> UploadAsync(ProfileRequestDtos requestDtos, CancellationToken cancellationToken = default)
    {
        var objectName = $"{Guid.NewGuid()}{Path.GetExtension(requestDtos.FileName)}";

        var putObjectArgs = new PutObjectArgs()
            .WithBucket(_settings.BucketName)
            .WithObject(objectName)
            .WithStreamData(requestDtos.Content)
            .WithObjectSize(requestDtos.Length)
            .WithContentType(requestDtos.ContentType);

        await minioClient.PutObjectAsync(putObjectArgs, cancellationToken);

        return objectName;
    }
    
    public async Task DeleteAsync(string objectName, CancellationToken cancellationToken = default)
    {
        var removeObjectArgs = new RemoveObjectArgs()
            .WithBucket(_settings.BucketName)
            .WithObject(objectName);

        await minioClient.RemoveObjectAsync(removeObjectArgs, cancellationToken);
    }
}