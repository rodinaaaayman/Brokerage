using Brokerage.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateAccessToken(Clients client);

        string GenerateRefreshToken();
    }
}
