using AutoMapper;
using Store.Business.Models.Orders;
using Store.Business.Services.Interfaces;
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

        public async Task<int?> Create(OrderModel orderModel)
        {
            var order = _mapper.Map<Order>(orderModel);

            return await _orderRepository.Create(order);
        }

        public async Task<int> Delete(int id)
        {
            return await _orderRepository.Delete(id);
        }

        public async Task<OrderModel?> Get(int id)
        {
            var order = await _orderRepository.Get(id);

            var model = _mapper.Map<OrderModel?>(order);

            return model;
        }

        public async Task<IEnumerable<OrderModel>> Get()
        {
            var orderDtos = await _orderRepository.Get();

            var ordersGrouped = orderDtos.GroupBy(o => o.Id).
                Select(group => new Order
                {
                    Id = group.Key,
                    OrderDate = group.First().OrderDate,
                    TotalAmount = group.First().TotalAmount,
                    UserId = group.First().UserId,
                    User = new User
                    {
                        FirstName = group.First().FirstName,
                        LastName = group.First().LastName,
                        Email = group.First().Email
                    },
                    OrderItems = group.GroupBy(oi => oi.OrderItemId)
                    .Select(orderItemGroup => new OrderItem
                    {
                        Id = orderItemGroup.Key,
                        BookId = orderItemGroup.First().BookId,
                        Book = new Book
                        {
                            Name = orderItemGroup.First().BookName
                        },
                        Price = orderItemGroup.First().Price,
                        Quantity = orderItemGroup.First().Quantity
                    }).ToList()
                });

            var models = _mapper.Map<IEnumerable<OrderModel>>(ordersGrouped);

            return models;
        }

        public async Task<OrderModel?> Update(OrderModel orderModel)
        {
            var order = _mapper.Map<Order>(orderModel);

            order = await _orderRepository.Update(order);

            var model = _mapper.Map<OrderModel>(order);

            return model;
        }
    }
}
