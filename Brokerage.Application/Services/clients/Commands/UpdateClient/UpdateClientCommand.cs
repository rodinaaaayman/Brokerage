using Brokerage.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using MediatR;
using Brokerage.Application.DTOs;

namespace Brokerage.Application.Services.clients.Commands.UpdateClient;

public record UpdateClientCommand(
    int Id,
    UpdateClientDTO Client
) : IRequest<bool>;
