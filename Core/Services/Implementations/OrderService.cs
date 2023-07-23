using AutoMapper;
using EdrakTask.Core.Domain;
using EdrakTask.Core.Dtos;
using EdrakTask.Core.Enums;
using EdrakTask.Core.Helpers;
using EdrakTask.Core.Persistence;
using EdrakTask.Core.Repositories;
using EdrakTask.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EdrakTask.Core.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Order> _repository;

        public OrderService(
            IMapper mapper,
            IRepository<Order> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public IEnumerable<OrderDto> GetOrders(int? count)
        {
            var orders = _repository.GetAll(count: count,
                asNoTracked: true,
                include: o => o.Include(i => i.Customer).Include(o=>o.Status)
                .Include(i => i.OrderLines).ThenInclude(io => io.Product)
                );

            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public void CancelOrder(Guid orderId)
        {
            Guards.GuidNotEmpty(orderId);

            var order = _repository.Get(orderId,
                asNoTracked: true,
                include: o => o.Include(i => i.OrderLines));

            Guards.EntityNotFound(order);

            var products = _repository.GetProducts(order.OrderLines.Select(o => o.ProductId).ToList()).ToDictionary(p=>p.Id);

            foreach (var item in order.OrderLines)
            {
                products[item.ProductId].Quantity += item.Quantity;
            }

            _repository.Remove(order);
            _repository.SaveChanges();
            
            
        }

        public OrderDto CreateOrder(OrderInputDto orderInput)
        {
            Guards.ArgumentNotNull(orderInput);

            var customerExist = _repository.CustomerExist(orderInput.CustomerId.Value);
            Guards.EntityNotFound(customerExist);

            var order = new Order()
            {
                CustomerId = orderInput.CustomerId.Value,
                Status = _repository.GetOrderStatus((int)OrderStatusEnum.Pending),
                OrderDate = DateTime.Now,
                OrderLines = new List<OrderLine>()
            };
            var productsIds = orderInput.OrderLines.Select(p => p.ProductId.Value).ToList();

            var allProduct = _repository.GetProducts(productsIds);

            Guards.SomeEntityNotFound(allProduct.Count(), productsIds.Count);
            decimal totalPrice = 0;
            foreach (var item in allProduct)
            {
                var amount = orderInput.OrderLines.FirstOrDefault(p => p.ProductId == item.Id).Amount;
                Guards.PrdouctAmountNotEnough(item.Quantity, amount.Value);
                order.OrderLines.Add(
                    new OrderLine()
                    {
                        Description= item.Description,
                        Name= item.Name,
                        Price= item.Price,
                        Quantity = amount.Value,
                        ProductId = item.Id
                    });
                order.Amount += (amount.Value *item.Price);
                item.Quantity -= amount.Value;
            }
            
            _repository.Add(order);
            _repository.SaveChanges();
            return _mapper.Map<OrderDto>(order);
        }

        public OrderDto GetOrder(Guid orderId)
        {
            Guards.GuidNotEmpty(orderId);
            var order = _repository.Get(orderId,
                asNoTracked: true,
                include: o => o.Include(i => i.Customer).Include(o => o.Status)
                .Include(i => i.OrderLines).ThenInclude(io => io.Product));

            return _mapper.Map<OrderDto>(order);
        }

        public void UpdateOrder(Guid orderId, OrderStatusEnum orderStatus)
        {
            Guards.GuidNotEmpty(orderId);
            var order = _repository.Get(orderId);
            Guards.EntityNotFound(order);
            
            order.StatusId = (int) orderStatus;

            _repository.SaveChanges();
        }

        public bool OrderExist(Guid orderId)
        {
            Guards.GuidNotEmpty(orderId);

            return _repository.Exist(orderId);
        }
    }
}
