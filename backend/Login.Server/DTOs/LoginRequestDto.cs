namespace Login.Server.DTOs;

public sealed record LoginRequestDto(string UserNameOrEmail, string Password);
