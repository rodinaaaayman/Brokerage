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
            var user = await _context.Users
                .FirstOrDefaultAsync(x =>
                    x.Email == request.Email);

            if (user == null)
                throw new Exception("Invalid credentials.");

            if (user.Password != request.Password)
                throw new Exception("Invalid credentials.");

            var accessToken =
                _jwt.GenerateAccessToken(user);     
            var refreshToken =
                _jwt.GenerateRefreshToken();

            user.RefreshToken = refreshToken;

            user.RefreshTokenExpiryTime =
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
