using EdrakTask.Core.Dtos;
using EdrakTask.Core.Enums;

namespace EdrakTask.Core.Services.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<OrderDto> GetOrders(int? count);
        OrderDto GetOrder(Guid orderId);

        OrderDto CreateOrder(OrderInputDto orderInput);

        void UpdateOrder(Guid orderId, OrderStatusEnum orderStatus);

        void CancelOrder(Guid orderId);

        bool OrderExist(Guid orderId);
    }
}
