using Brokerage.Application.DTOs;
using MediatR;

namespace Brokerage.Application.Services.clients.Commands.UpdateClient;

public record UpdateClientCommand(
    int Id,
    UpdateClientDTO Client
) : IRequest<bool>;
