namespace SocialService.Application.DTOs.Requests;

public record UploadAvatarRequest(Stream Content, string FileName, string ContentType, long Length) { }