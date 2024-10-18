using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stope.Api.Models.Requests;
using Store.Business.Models.Orders;
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

        [HttpPost]
        public async Task<int> Create([FromBody] OrderRequestModel order)
        {
            var model = _mapper.Map<OrderModel>(order);

            return await _orderService.Create(model);
        }

        [HttpGet("{id}")]
        public async Task<OrderViewModel> Get(int id)
        {
            var orderDto = await _orderService.Get(id);

            var model = _mapper.Map<OrderViewModel>(orderDto);
            model.Status = 1;

            return model;
        }

        [HttpGet]
        public async Task<IEnumerable<OrderViewModel>> Get()
        {
            var orderDtos = await _orderService.Get();

            var models = _mapper.Map<IEnumerable<OrderViewModel>>(orderDtos);

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

        [HttpPut("{id}")]
        public async Task<OrderViewModel> Update(int id, [FromBody] OrderRequestModel orderModel)
        {
            orderModel.Id = id;
            var model = _mapper.Map<OrderModel>(orderModel);

            var order = await _orderService.Update(model);

            var viewModel = _mapper.Map<OrderViewModel>(order);
            viewModel.Status = 1;

            return viewModel;
        }
    }
}
