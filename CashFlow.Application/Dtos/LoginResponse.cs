namespace CashFlow.Application.Dtos;
public record LoginResponse(
    string AccessToken,
    Guid? UserId,
    Guid? RoleId,
    string Username);
