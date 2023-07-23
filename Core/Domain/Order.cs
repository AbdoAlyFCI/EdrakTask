//using EdrakTask.Core.Enums;

namespace EdrakTask.Core.Domain
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        public DateTime OrderDate { get; set; }
        public decimal Amount { get; set; }

        public int StatusId { get; set; }
        public OrderStatus Status { get; set; }

        public ICollection<OrderLine> OrderLines { get; set; }

    }
}
