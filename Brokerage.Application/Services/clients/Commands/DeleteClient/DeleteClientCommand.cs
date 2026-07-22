using MediatR;
namespace Brokerage.Application.Services.clients.Commands.DeleteClient;


public record DeleteClientCommand(int Id) : IRequest<bool>;
