using System.Security.Claims;
using Brokerage.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Brokerage.WebApi.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(
        IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }


    public int Id
    {
        get
        {
            var userId = _httpContextAccessor
                .HttpContext?
                .User
                .FindFirstValue(ClaimTypes.NameIdentifier);

            return int.Parse(userId!);
        }
    }


    public bool IsAdmin =>
        _httpContextAccessor
            .HttpContext!
            .User
            .IsInRole("Admin");
}