using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Store.Business.Services.Interfaces;
using Store.ViewModels.OrderViewModels;

namespace Stope.Api.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public OrderViewModel Get(int id)
        {
            var order = _orderService.Get(id);

            var model = _mapper.Map<OrderViewModel>(order);
            model.Status = 1;

            return model;
        }
    }
}
