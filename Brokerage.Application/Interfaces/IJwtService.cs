using Brokerage.Models;

namespace Brokerage.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateAccessToken(Users User);

        string GenerateRefreshToken();
    }
}
