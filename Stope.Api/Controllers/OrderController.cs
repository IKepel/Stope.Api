using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Store.Business.Services.Interfaces;
using Store.Data.Requests;
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

        [HttpPost]
        public async Task<int> Create(UpsertOrderRequestModel order)
        {
            return await _orderService.Create(order);
        }

        [HttpGet("{id}")]
        public async Task<OrderViewModel> Get(int id)
        {
            var order = await _orderService.Get(id);

            var model = _mapper.Map<OrderViewModel>(order);
            model.Status = 1;

            return model;
        }

        [HttpGet]
        public async Task<IEnumerable<OrderViewModel>> Get()
        {
            var orders = await _orderService.Get();

            var models = _mapper.Map<IEnumerable<OrderViewModel>>(orders);

            foreach (var model in models)
            {
                model.Status = 1;
            }

            return models;
        }

        [HttpDelete("{id}")]
        public async Task<int> Delete(int id)
        {
            return await _orderService.Delete(id);
        }

        [HttpPut("[action]")]
        public async Task<OrderViewModel> Update(UpsertOrderRequestModel orderModel)
        {
            var order = await _orderService.Update(orderModel);

            var model = _mapper.Map<OrderViewModel>(order);
            model.Status = 1;

            return model;
        }
    }
}
