using AutoMapper;
using Store.Business.Models.Orders;
using Store.Business.Services.Interfaces;
using Store.Data.Dtos;
using Store.Data.Entities;
using Store.Data.Repositories.Iterfaces;

namespace Store.Business.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<int> Create(OrderModel orderModel)
        {
            var order = _mapper.Map<Order>(orderModel);

            return await _orderRepository.Create(order);
        }

        public async Task<int> Delete(int id)
        {
            return await _orderRepository.Delete(id);
        }

        public async Task<OrderDto> Get(int id)
        {
            var order = await _orderRepository.Get(id);

            var orderDto = _mapper.Map<OrderDto>(order);

            return orderDto;
        }

        public async Task<IEnumerable<OrderDto>> Get()
        {
            var orders = await _orderRepository.Get();

            var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(orders);

            return orderDtos;
        }

        public async Task<OrderDto> Update(OrderModel orderModel)
        {
            var order = _mapper.Map<Order>(orderModel);

            order = await _orderRepository.Update(order);

            var model = _mapper.Map<OrderDto>(order);

            return model;
        }
    }
}
