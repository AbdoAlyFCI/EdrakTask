using EdrakTask.Core.Dtos;

namespace EdrakTask.Core.Services.Interfaces
{
    public interface ICustomerService
    {
        IEnumerable<OrderDto> GetOrders(Guid customerId);
    }
}
