using tadoba_api.Models;

namespace tadoba_api.Services
{
    public interface IOrderService
    {
        Task<Response<CartModel>> AddCart(CartModel cart);
        Task<Response<CartModel>> UpdateCart(CartModel cart);
        Task<Response<bool>> DeleteCart(List<long> ids);
        Task<Response<List<CartModel>>> GetCartData(long userId);
        Task<Response<bool>> PlaceOrder(PlaceOrderModel placeOrderModel);
        Task<Response<List<OrderModel>>> GetAllOrders(long userId);
        Task<Response<List<OrderDetailsModel>>> GetOrderDetails(long orderId);
        Task<Response<List<OrderModel>>> GetAllOrders();
        Task<Response<List<OrderModel>>> GetAllUnDeliveredOrders();
    }
}
