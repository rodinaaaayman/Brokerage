using System;
using System.Collections.Generic;
using System.Text;
using Brokerage.Application.DTOs;
using global::Brokerage.DTOs;
using MediatR;

namespace Brokerage.Application.Services.clients.Queries.GetClients;

public record GetClientsQuery : IRequest<IEnumerable<ClientsDTO>>;

