using EdrakTask.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EdrakTask.Core.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<Order> GetAll(
            bool asNoTracked = false,
            int? count = null,
            Expression<Func<Order, bool>> predicate = null,
            Func<IQueryable<Order>, IQueryable<Order>> include = null
            );
        T Get(Guid orderId,
            bool asNoTracked = false,
              Func<IQueryable<Order>, IQueryable<Order>> include = null);
        IQueryable<Product> GetProducts(List<Guid> productsIds);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);

        bool Exist(Guid id);
        bool CustomerExist(Guid customerId);
        int SaveChanges();
        OrderStatus GetOrderStatus(int statusId);
    }
}
