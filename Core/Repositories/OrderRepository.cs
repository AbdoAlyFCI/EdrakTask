using EdrakTask.Core.Domain;
using EdrakTask.Core.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace EdrakTask.Core.Repositories
{
    public class OrderRepository
    : IRepository<Order>
    {
        private readonly IAppDbContext _dbContext;
        public OrderRepository(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Order> GetAll(
            bool asNoTracked = false,
            int? count = null,
            Expression<Func<Order, bool>> predicate = null,
            Func<IQueryable<Order>, IQueryable<Order>> include = null
            )
        {
            var query = _dbContext.Orders.AsQueryable();
            if(asNoTracked)
            {
                query = query.AsNoTracking();
            }
            if (include != null)
            {
                query = include(query);
            }
            if(predicate!= null)
            {
                query = query.Where(predicate);
            }
            if (count != null)
            {
                query = query.Take(count.Value);
            }

            return query;
        }

        public void Add(Order entity)
        {
            _dbContext.Orders.Add(entity);
        }

        public void Remove(Order entity)
        {
            _dbContext.Orders.Remove(entity);
        }

        public Order Get(Guid orderId,
                        bool asNoTracked = false,
                        Func<IQueryable<Order>, IQueryable<Order>> include = null)
        {
            var order = _dbContext.Orders.AsQueryable();
            if (asNoTracked)
            {
                order = order.AsNoTracking();
            }
            if (include != null)
            {
                order = include(order);
            }


            return order.FirstOrDefault(o => o.Id == orderId);
        }

        public IQueryable<Product> GetProducts(List<Guid> productsIds)
        {
            return _dbContext.Products.Where(p => productsIds.Contains(p.Id));
        }

        public void Update(Order entity)
        {
        }
        public bool Exist(Guid id)
        {
            return _dbContext.Orders.Any(o=>o.Id==id);
        }
        public bool CustomerExist(Guid customerId)
        {
            return _dbContext.Customers.Any(c => c.Id == customerId);
        }
        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public OrderStatus GetOrderStatus(int statusId)
        {
            return _dbContext.OrderStatus.FirstOrDefault(os => os.Id == statusId);
        }     
    }
}
