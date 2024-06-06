using ClickNPick.Domain.Models;


namespace ClickNPick.Application.Abstractions.Services;

public interface ITokenGeneratorService
{
    public Task<string> GenerateToken(User user);
}
