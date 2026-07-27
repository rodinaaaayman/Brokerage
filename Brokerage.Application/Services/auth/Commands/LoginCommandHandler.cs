using Brokerage.Application.DTOs.Auth;
using Brokerage.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Brokerage.Application.Services.auth.Commands
{
    public class LoginCommandHandler
    : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IJwtService _jwt;

        public LoginCommandHandler(
            IApplicationDbContext context,
            IJwtService jwt)
        {
            _context = context;
            _jwt = jwt;
        }

        public async Task<LoginResponse> Handle(
            LoginCommand request,
            CancellationToken cancellationToken)
        {
            var client = await _context.Clients
                .FirstOrDefaultAsync(x =>
                    x.Email == request.Email);

            if (client == null)
                throw new Exception("Invalid credentials.");

            if (client.Password != request.Password)
                throw new Exception("Invalid credentials.");

            var accessToken =
                _jwt.GenerateAccessToken(client);

            var refreshToken =
                _jwt.GenerateRefreshToken();

            client.RefreshToken = refreshToken;

            client.RefreshTokenExpiryTime =
                DateTime.UtcNow.AddDays(7);

            await _context.SaveChangesAsync(cancellationToken);

            return new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}
