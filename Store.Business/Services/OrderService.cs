using AutoMapper;
using Store.Business.Models.Orders;
using Store.Business.Services.Interfaces;
using Store.Data.Entities;
using Store.Data.Repositories.Iterfaces;
using Store.Data.Requests;

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

        public async Task<int> Create(UpsertOrderRequestModel orderModel)
        {
            var order = _mapper.Map<Order>(orderModel);
            return await _orderRepository.Create(order);
        }

        public async Task<int> Delete(int id)
        {
            return await _orderRepository.Delete(id);
        }

        public async Task<OrderModel> Get(int id)
        {
            var order = await _orderRepository.Get(id);

            var model = _mapper.Map<OrderModel>(order);

            return model;
        }

        public async Task<IEnumerable<OrderModel>> Get()
        {
            var orders = await _orderRepository.Get();

            var models = _mapper.Map<IEnumerable<OrderModel>>(orders);

            return models;
        }

        public async Task<OrderModel> Update(UpsertOrderRequestModel orderModel)
        {
            var order = _mapper.Map<Order>(orderModel);

            order = await _orderRepository.Update(order);

            var model = _mapper.Map<OrderModel>(order);

            return model;
        }
    }
}
