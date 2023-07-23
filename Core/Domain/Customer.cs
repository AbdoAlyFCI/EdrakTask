using System.ComponentModel.DataAnnotations;

namespace EdrakTask.Core.Domain
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }


    }
}
