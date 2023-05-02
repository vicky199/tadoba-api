using AutoMapper;
using tadoba_api.Entity;
using tadoba_api.Models;
using tadoba_api.Repository;
using tadoba_api.Uow;

namespace tadoba_api.Services
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Order> _orderRepo;
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<Cart> _cartRepo;
        private readonly IGenericRepository<OrderDetails> _orderDetailsRepo;
        private readonly IGenericRepository<AppConfig> _appConfigRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IGenericRepository<Order> orderRepo, IGenericRepository<Cart> cartRepo, IGenericRepository<OrderDetails> orderDetailsRepo
                            , IMapper mapper, IUnitOfWork unitOfWork, IGenericRepository<Product> productRepo, IGenericRepository<AppConfig> appConfigRepo)
        {
            _orderRepo = orderRepo;
            _cartRepo = cartRepo;
            _orderDetailsRepo = orderDetailsRepo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _productRepo = productRepo;
            _appConfigRepo = appConfigRepo;
        }
        public async Task<Response<CartModel>> AddCart(CartModel cart)
        {
            Cart cartData = _mapper.Map<Cart>(cart);
            cartData = await _cartRepo.AddAsync(cartData);
            await _unitOfWork.SaveAsync();
            cart = _mapper.Map<CartModel>(cart);
            return await Response<CartModel>.GenerateResponse(true, cart, "Product added to cart Successfully.");
        }

        public async Task<Response<bool>> DeleteCart(List<long> ids)
        {
            IEnumerable<Cart> existingCart = await _cartRepo.ListAllAsync(x => ids.Contains(x.Id));
            await _cartRepo.DeleteRangeAsync(existingCart);
            await _unitOfWork.SaveAsync();
            return await Response<bool>.GenerateResponse(true, true, "Product removed to cart Successfully.");
        }

        public async Task<Response<List<OrderModel>>> GetAllOrders(long userId)
        {
            IEnumerable<Order> orders = (await _orderRepo.ListAllAsync(x => x.UserId == userId, null, "UserAddresses")).OrderByDescending(x => x.OrderDate);
            IEnumerable<OrderModel> response = _mapper.Map<IEnumerable<OrderModel>>(orders);
            return await Response<List<OrderModel>>.GenerateResponse(true, response.ToList());
        }

        public async Task<Response<List<OrderModel>>> GetAllOrders()
        {
            IEnumerable<Order> orders = (await _orderRepo.ListAllAsync(null, null, "UserAddresses")).OrderByDescending(x => x.OrderDate);
            IEnumerable<OrderModel> response = _mapper.Map<IEnumerable<OrderModel>>(orders);
            return await Response<List<OrderModel>>.GenerateResponse(true, response.ToList());
        }
        public async Task<Response<List<OrderModel>>> GetAllUnDeliveredOrders()
        {
            IEnumerable<Order> orders = (await _orderRepo.ListAllAsync(x => x.DeliveryDate == null, null, "UserAddresses")).OrderByDescending(x => x.OrderDate);
            IEnumerable<OrderModel> response = _mapper.Map<IEnumerable<OrderModel>>(orders);
            return await Response<List<OrderModel>>.GenerateResponse(true, response.ToList());
        }

        public async Task<Response<List<CartModel>>> GetCartData(long userId)
        {
            IEnumerable<Cart> cartData = await _cartRepo.ListAllAsync(x => x.UserId == userId, null, "Product");
            IEnumerable<CartModel> response = _mapper.Map<IEnumerable<CartModel>>(cartData);
            return await Response<List<CartModel>>.GenerateResponse(true, response.ToList());
        }

        public async Task<Response<List<OrderDetailsModel>>> GetOrderDetails(long orderId)
        {
            List<OrderDetails> orderData = (await _orderDetailsRepo.ListAllAsync(x => x.OrderId == orderId, null, "Product")).ToList();
            List<OrderDetailsModel> result = _mapper.Map<List<OrderDetailsModel>>(orderData);
            return await Response<List<OrderDetailsModel>>.GenerateResponse(true, result);
        }

        public async Task<Response<bool>> PlaceOrder(PlaceOrderModel placeOrderModel)
        {
            string expectedDelivery = (await _appConfigRepo.GetByFilterAsync(x => x.Type == enumAppConfig.ExpectedDelivery.ToString())).Value;
            Order order = new Order();
            order.OrderNo = Guid.NewGuid().ToString();
            order.UserId = placeOrderModel.UserId;
            order.AddressId = placeOrderModel.AddressId;
            order.TransactionId = placeOrderModel.TransactionId;
            order.Total = placeOrderModel.Total;
            order.Discount = placeOrderModel.Discount;
            order.OrderDate = DateTime.Now;
            order.ExpectedDeliveryDate = DateTime.Now.AddDays(int.Parse(expectedDelivery));
            order = await _orderRepo.AddAsync(order);
            foreach (long cartId in placeOrderModel.CartIds)
            {
                Cart cart = await _cartRepo.GetByIdAsync(cartId);
                OrderDetails orderDetails = new OrderDetails();
                orderDetails.ProductId = cart.ProductId;
                orderDetails.Quantity = cart.Quantity;
                orderDetails.Price = cart.Price;
                orderDetails.Discount = cart.Discount;
                orderDetails.OrderId = order.Id;
                orderDetails = await _orderDetailsRepo.AddAsync(orderDetails);
            }
            await _unitOfWork.SaveAsync();
            return await Response<bool>.GenerateResponse(true, true, "Order Placed successfully.");
        }

        public async Task<Response<CartModel>> UpdateCart(CartModel cart)
        {
            Cart cartData = _mapper.Map<Cart>(cart);
            if (cart.Quantity <= 0)
            {
                await _cartRepo.DeleteAsync(cartData);
            }
            else
            {
                await _cartRepo.UpdateAsync(cartData);
            }
            await _unitOfWork.SaveAsync();
            cart = _mapper.Map<CartModel>(cart);
            return await Response<CartModel>.GenerateResponse(true, cart, "Cart Updated Successfully.");
        }
    }
}
