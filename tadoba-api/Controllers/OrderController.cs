using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tadoba_api.Models;
using tadoba_api.Services;

namespace tadoba_api.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost]
        public async Task<IActionResult> AddCart(CartModel cart)
        {
            return Ok(await _orderService.AddCart(cart));
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCart(CartModel cart)
        {
            return Ok(await _orderService.UpdateCart(cart));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCart([FromBody] List<long> ids)
        {
            return Ok(await _orderService.DeleteCart(ids));
        }
        [HttpGet]
        public async Task<IActionResult> GetCartData()
        {
            long userId = await GetUserId();
            return Ok(await _orderService.GetCartData(userId));
        }
        [HttpPost]
        public async Task<IActionResult> PlaceOrder(PlaceOrderModel placeOrderModel)
        {
            placeOrderModel.UserId = await GetUserId();
            return Ok(await _orderService.PlaceOrder(placeOrderModel));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            long userId = await GetUserId();
            return Ok(await _orderService.GetAllOrders(userId));
        }
        [HttpGet]
        public async Task<IActionResult> GetOrderDetails(long orderId)
        {
            return Ok(await _orderService.GetOrderDetails(orderId));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrdersForAdmin()
        {
            return Ok(await _orderService.GetAllOrders());
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUnDeliveredOrders()
        {
            return Ok(await _orderService.GetAllUnDeliveredOrders());
        }
    }
}
