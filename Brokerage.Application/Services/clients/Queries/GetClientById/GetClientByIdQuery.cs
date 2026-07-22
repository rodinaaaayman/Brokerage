using Brokerage.DTOs;
using MediatR;
namespace Brokerage.Application.Services.clients.Queries.GetClientById
{
    public record GetClientByIdQuery(int Id) : IRequest<ClientsDTO?>;
}
