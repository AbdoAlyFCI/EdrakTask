using EdrakTask.Core.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace EdrakTask.Core.Dtos
{
    public class OrderInputDto
    {
        [Required]
        public Guid? CustomerId { get; set; }

        [OrderListNotEmpty(ErrorMessage ="Order Line is empty")]
        public IEnumerable<OrderLineInputDto>? OrderLines { get; set; }

    }
}
