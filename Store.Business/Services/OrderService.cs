using AutoMapper;
using Store.Business.Models.Orders;
using Store.Business.Services.Interfaces;
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

        public OrderModel Get(int id)
        {
            var order = _orderRepository.Get(id);

            var model = _mapper.Map<OrderModel>(order);

            return model;
        }
    }
}
