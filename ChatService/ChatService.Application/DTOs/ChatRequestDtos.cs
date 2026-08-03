namespace ChatService.Application.DTOs;

public record SendMessageRequest(Guid ChatId, string Content);