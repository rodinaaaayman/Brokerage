using Brokerage.Models;
using Microsoft.EntityFrameworkCore;


namespace Brokerage.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Clients> Clients { get; }
        DbSet<Orders> Orders { get; }
        DbSet <Executions> Executions { get; }
        DbSet<Invoice> Invoices { get; }


        Task<int> SaveChangesAsync(
            CancellationToken cancellationToken);
    }
}
