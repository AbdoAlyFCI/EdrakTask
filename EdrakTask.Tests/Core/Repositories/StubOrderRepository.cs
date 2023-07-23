using EdrakTask.Core.Repositories;
using System.Linq.Expressions;

namespace EdrakTask.Tests.Core.Repositories
{
    internal class StubOrderRepository : IRepository<Order>
    {
        public List<Order> _orders;
        public List<Customer> _customers;
        public List<Product> _products;
        public List<OrderStatus> _orderStatus;
        public StubOrderRepository()
        {
            _customers = new();
            _products = new();
            _orders = new();
            _orderStatus = new()
            {
                new OrderStatus(){Id= 1},
                new OrderStatus(){Id= 2},
                new OrderStatus(){Id= 3},
            };
        }
        public void Add(Order entity)
        {
            _orders.Add(entity);
        }

        public bool CustomerExist(Guid customerId)
        {
            return _customers.Any(c => c.Id == customerId);
        }

        public bool Exist(Guid id)
        {
            return _orders.Any(o => o.Id == id);
        }

        public Order Get(Guid orderId, bool asNoTracked = false, Func<IQueryable<Order>, IQueryable<Order>> include = null)
        {
            return _orders.FirstOrDefault(o => o.Id == orderId);
        }

        public IQueryable<Order> GetAll(bool asNoTracked = false, int? count = null, Expression<Func<Order, bool>> predicate = null, Func<IQueryable<Order>, IQueryable<Order>> include = null)
        {
            var query = _orders;
            if (count != null)
            {
                query = query.Take(count.Value).ToList();
            }

            return query.AsQueryable();
        }

        public OrderStatus GetOrderStatus(int statusId)
        {
            return _orderStatus.FirstOrDefault(os => os.Id == statusId);
        }

        public IQueryable<Product> GetProducts(List<Guid> productsIds)
        {
            return _products.Where(p => productsIds.Contains(p.Id)).AsQueryable();
        }

        public void Remove(Order entity)
        {
            _orders.Remove(entity);
        }

        public int SaveChanges()
        {
            return 1;
        }

        public void Update(Order entity)
        {
        }
    }
}
