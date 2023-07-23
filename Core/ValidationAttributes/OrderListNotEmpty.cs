using EdrakTask.Core.Dtos;
using System.ComponentModel.DataAnnotations;

namespace EdrakTask.Core.ValidationAttributes
{
    internal class OrderListNotEmpty : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var order = (OrderInputDto)validationContext.ObjectInstance;

            if (order.OrderLines == null || order.OrderLines.Count() == 0)
            {
                return new ValidationResult(ErrorMessage,
                    new[] { nameof(OrderInputDto) });
            }

            return ValidationResult.Success;
        }
    }
}
