using AutoMapper;
using BLL.IServices;
using Dal;
using Dal.IRepositories;
using Dal.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrderBll : IOrderBll
    {
        IOrderDal dal;
        public OrderBll(IOrderDal dal)
        {
            this.dal = dal;

        }
        public async Task<Order> AddOrder(Order order)
        {
            Order res = await dal.AddOrder(order);
            return res;
        }

        public async Task<List<Order>> getAllOrders()
        {
            return await dal.getAllOrders();
        }

        public Task<Order> getOrderById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
