using AutoMapper;
using BLL.IServices;
using Dal.IRepositories;
using Dal.Models;
using DTO.Classes;


namespace BLL.Services
{
    public class OrderBll : IOrderBll
    {
        IOrderDal dal;
        IStockDal stockDal;
        IMapper mapper;
        public OrderBll(IOrderDal dal, IStockDal stockDal)
        {
            this.dal = dal;
            var config = new MapperConfiguration(cfg =>
            cfg.AddProfile<MarketProfile>());
            mapper = config.CreateMapper();
            this.stockDal = stockDal;
        }
        public async Task<OrderDto?> AddOrder(OrderDto order)
        {
            Order newOrder = await dal.AddOrder(mapper.Map<OrderDto, Order>(order));
            if (newOrder != null)
            {
                return mapper.Map<Order, OrderDto>(newOrder);
            }
            return null;
        }

        public async Task<List<OrderDto>> getAllOrders()
        {
            List<Order> allOrders = await dal.getAllOrders();
            return mapper.Map<List<Order>,List<OrderDto>>(allOrders);
        }

        public async Task<OrderDto?> getOrderById(int id)
        {
            Order? res = await dal.getOrderById(id);
            if(res != null) {
                return mapper.Map<Order,OrderDto>(res);
            }
            return null;
        }

        public async Task<bool> UpdateOrder(OrderDto order)
        {
            return await dal.UpdateOrder(mapper.Map<OrderDto, Order>(order));
        }
  }
}
