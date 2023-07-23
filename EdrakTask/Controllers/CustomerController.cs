using EdrakTask.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EdrakTask.API.Controllers
{
    [Route("api/Customer/{customerId}/orders")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        /// <summary>
        /// Get list of customer orders
        /// </summary>
        /// <remarks>
        /// Sample request
        /// 
        ///     GET api/Customer/93b98830-a77b-4a64-8891-6bfdce651e0b/orders
        ///     {
        ///     }
        /// </remarks>
        /// <returns>Order list</returns>
        [HttpGet]
        public IActionResult GetOrders(Guid customerId)
        {
            var orders = _customerService.GetOrders(customerId);
            return Ok(orders);
        }
    }
}
