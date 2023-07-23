
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdrakTask.Core.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }

        public DateTime OrderDate { get; set; }
        public int Amount { get; set; }

        public string StatusName { get; set; }

        public ICollection<OrderLineDto> OrderLines { get; set; }
    }
}
