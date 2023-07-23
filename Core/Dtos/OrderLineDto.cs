using EdrakTask.Core.Domain;

namespace EdrakTask.Core.Dtos
{
    public class OrderLineDto
    {
        public string ProductName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
