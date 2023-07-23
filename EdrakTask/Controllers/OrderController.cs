using EdrakTask.Core.Dtos;
using EdrakTask.Core.Enums;
using EdrakTask.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EdrakTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        /// <summary>
        /// Get List of order with ability to limit the number of records
        /// </summary>
        /// <param name="count"></param>      
        [HttpGet("GetAllOrders")]
        public ActionResult<IEnumerable<OrderDto>> GetAllOrderd(int? count)
        {
            if (count < 0)
            {
                return NotFound();
            }
            var orders = _orderService.GetOrders(count);
            return Ok(orders);
        }

        /// <summary>
        /// Get specific order based on its ID
        /// </summary>
        [HttpGet("GetOrder")]
        public ActionResult<OrderDto> GetOrder(Guid orderId)
        {
            if (!_orderService.OrderExist(orderId))
            {
                return NotFound();
            }

            var order = _orderService.GetOrder(orderId);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }


        /// <summary>
        /// Create new order.
        /// </summary>
        /// <remarks>
        /// Sample request
        /// 
        ///     POST api​/Order
        ///     {
        ///         "customerId": "93b98830-a77b-4a64-8891-6bfdce651e0b",
        ///         "orderLines": [
        ///             {
        ///                 "productId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "amount": 4
        ///             }
        ///         ]
        ///     }
        /// </remarks>
        /// <param name="orderInput"></param>      
        /// <returns>Order</returns>
        [HttpPost]
        public ActionResult<OrderDto> CreateOrder(OrderInputDto orderInput)
        {
            
                var order = _orderService.CreateOrder(orderInput);
                return Created("GetOrder",
                    new
                    {
                        orderId = Guid.NewGuid(),
                        order
                    });
            

        }

        /// <summary>
        /// Change Order Status
        /// </summary>
        /// <param name="orderId"></param>      
        /// <param name="orderStatus"></param>      

        [HttpPatch]
        public IActionResult UpdateOrder(Guid orderId, OrderStatusEnum orderStatus)
        {
            if (!_orderService.OrderExist(orderId))
            {
                return NotFound();
            }
            _orderService.UpdateOrder(orderId, orderStatus);
            return NoContent();
        }


        /// <summary>
        /// Cancel Order
        /// </summary>
        /// <param name="orderId"></param>        
        [HttpDelete]
        public IActionResult CancelOrder(Guid orderId)
        {
            if (!_orderService.OrderExist(orderId))
            {
                return NotFound();
            }
            _orderService.CancelOrder(orderId);
            return NoContent();
        }
    }
}
