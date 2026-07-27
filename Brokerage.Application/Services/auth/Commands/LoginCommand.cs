using Brokerage.Application.DTOs.Auth;
using MediatR;

public record LoginCommand(
    string Email,
    string Password)
    : IRequest<LoginResponse>;