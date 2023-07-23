using System.ComponentModel.DataAnnotations;

namespace EdrakTask.Core.Dtos
{
    public class OrderLineInputDto
    {
        [Required]
        public Guid? ProductId { get; set; }
        [Required]
        [Range(1,int.MaxValue)]
        public int? Amount { get; set; }

    }
}
