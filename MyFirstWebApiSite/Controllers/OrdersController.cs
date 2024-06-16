using AutoMapper;
using DTOs;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Security.Claims;

namespace MyFirstWebApiSite.Controllers
{
    public class OrdersController : Controller
    {
        private IOrderService _orderService;
        private IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
         {
            _orderService = orderService;
            _mapper = mapper;
        }

        // POST: OrdersController/Create
        [Route("api/[controller]")]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<OrderDto>> AddOrder([FromBody]OrderDto order)
        {
            try
            {
                //List<OrderItem> classList = order.OrderItems(dto => _mapper.Map<OrderItemDto,OrderItem>(dto)).ToList();
                Order regolarOrder = _mapper.Map<OrderDto, Order>(order);
                Order order1 = await _orderService.AddOrder(regolarOrder);
                OrderDto DtoOrder = _mapper.Map< Order,OrderDto>(order1);
                if (DtoOrder != null)
                    return Ok(DtoOrder);
                return BadRequest();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


    }
}
