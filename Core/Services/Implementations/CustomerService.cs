using AutoMapper;
using EdrakTask.Core.Domain;
using EdrakTask.Core.Dtos;
using EdrakTask.Core.Repositories;
using EdrakTask.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EdrakTask.Core.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Order> _repository;


        public CustomerService(IMapper mapper, IRepository<Order> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public IEnumerable<OrderDto> GetOrders(Guid customerId)
        {
            var orders = _repository.GetAll(
                asNoTracked: true,
                include: o => o.Include(i => i.Customer).Include(o => o.Status)
                .Include(i => i.OrderLines).ThenInclude(io => io.Product),
                predicate: o => o.CustomerId == customerId
                );

            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }
    }
}
