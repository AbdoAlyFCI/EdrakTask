using EdrakTask.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdrakTask.Infrastructure.Database
{
    public class DbInitializer
    {
        public static void Initialize(AppDbContext dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
            dbContext.Database.EnsureCreated();
            if (dbContext.Customers.Any() && dbContext.Products.Any()) return;

            var customer = new Customer()
            {
                Id = new Guid("a4ad7184-a237-4eb8-a651-190116b98cbe"),
                Name = "Test",
                Address = "Address",
                Email = "Email"
            };

            dbContext.Customers.Add(customer);

            var product1 = new Product()
            {
                Id = new Guid("4920e57f-5bb6-4c45-bda9-77e2336084c1"),
                Name = "P1",
                Description= "Test",
                Price = 1000,
                Quantity = 20
            };


            var product2 = new Product()
            {
                Id = new Guid("32514b50-4d4c-49ee-827d-5f938f6f349b"),
                Name = "P2",
                Description = "Test",
                Price = 500,
                Quantity = 2000
            };

            dbContext.Products.Add(product1);
            dbContext.Products.Add(product2);

            dbContext.SaveChanges();
        }
    }
}
