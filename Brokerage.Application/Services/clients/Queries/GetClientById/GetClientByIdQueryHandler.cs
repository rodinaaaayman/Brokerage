using Brokerage.Application.Interfaces;
using Brokerage.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;


namespace Brokerage.Application.Services.clients.Queries.GetClientById;


public class GetClientByIdQueryHandler
    : IRequestHandler<GetClientByIdQuery, ClientsDTO?>
{

    private readonly IApplicationDbContext _context;
    private readonly IMemoryCache _cache;


    public GetClientByIdQueryHandler(IApplicationDbContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
    }


    public async Task<ClientsDTO?> Handle(
        GetClientByIdQuery request,
        CancellationToken cancellationToken)
    {
        var cacheKey = $"client_{request.Id}";

        if (_cache.TryGetValue(cacheKey, out ClientsDTO? cachedClient))
        {
            return cachedClient;
        }

        var client = await _context.Clients
            .Where(c => c.Id == request.Id && c.IsActive)
            .Select(c => new ClientsDTO
            {
                Username = c.Username,
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                NationalID = c.NationalID,
                PhoneNumber = c.PhoneNumber,
                AccountBalance = c.AccountBalance
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (client == null)
        {
            return null;
        }

        _cache.Set(
       cacheKey,
       client,
       new MemoryCacheEntryOptions
       {
           SlidingExpiration = TimeSpan.FromMinutes(5)
       });

        return client;
    }
}