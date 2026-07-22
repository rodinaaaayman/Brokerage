using Brokerage.DTOs;
using MediatR;

namespace Brokerage.Application.Services.clients.Queries.GetClients;

public record GetClientsQuery : IRequest<IEnumerable<ClientsDTO>>;

