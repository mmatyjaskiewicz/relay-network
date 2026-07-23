namespace SocialService.Application.DTOs.Requests;

public record ProfileRequestDtos(Stream Content, string FileName, string ContentType, long Length) { }

public record UpdateBioRequestDto(string Bio) { }