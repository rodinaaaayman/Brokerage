using Brokerage.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;


namespace Brokerage.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Users> Users { get; }
        DbSet<Clients> Clients { get; }
        DbSet<Orders> Orders { get; }
        DbSet <Executions> Executions { get; }
        DbSet<Invoice> Invoices { get; }
        DatabaseFacade Database { get; }

        Task<int> SaveChangesAsync(
            CancellationToken cancellationToken);
    }
}
