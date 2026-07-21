using Brokerage.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Clients> Clients { get; }
        DbSet<Brokerage.Models.Orders> Orders { get; }

        Task<int> SaveChangesAsync(
            CancellationToken cancellationToken);
    }
}
