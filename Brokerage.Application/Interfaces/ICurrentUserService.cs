namespace Brokerage.Application.Interfaces;

public interface ICurrentUserService
{
    int Id { get; }
    bool IsAdmin { get; }
}
