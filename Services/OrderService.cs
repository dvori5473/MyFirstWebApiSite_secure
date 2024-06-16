using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MyFirstWebApiSite;
using Repositories;

namespace Services
{
    public class OrderService : IOrderService
    {
        private IOrderRepository _orderRepository;
        private IProductService _productService ;
        private readonly ILogger<OrderService> _Ilogger;

        public OrderService(IOrderRepository orderRepository,IProductService productService,ILogger<OrderService> Ilogger)
        {
            _orderRepository = orderRepository;
            _productService = productService;
            _Ilogger = Ilogger;
        }

        public async Task<Order> AddOrder(Order order)
        {
            var sum = 0;
            foreach (OrderItem item in order.OrderItems)
            {
                Product product = await _productService.GetById(item.ProductId);
                sum += product.Price * item.Quantitiy;
            }
            if (sum != order.OrderSum) 
            {
                _Ilogger.LogError($"user {order.UserId}  tried perchasing with a difffrent price {order.OrderSum} instead of {sum}");
                _Ilogger.LogInformation($"user {order.UserId}  tried perchasing with a difffrent price {order.OrderSum} instead of {sum}");
            }
          
                order.OrderSum = sum;
                return await _orderRepository.AddOrder(order);
            
        }

    }
}
