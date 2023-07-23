using EdrakTask.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdrakTask.Core.Persistence
{
    public interface IAppDbContext
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderStatus> OrderStatus { get; set; }

        DatabaseFacade Database { get; }
        public int SaveChanges();
    }
}
