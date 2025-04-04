using BLL.IServices;
using Dal;
using Dal.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrderStockBll : IOrderStockBll
    {
        IOrderStockDal dal;
        public OrderStockBll(IOrderStockDal dal)
        {
            this.dal = dal;
        }
        public async Task<OrderStock> AddOrderStock(OrderStock orderStock)
        {
            var res= await dal.AddOrderStock(orderStock);
            return res;
        }
        public async Task<List<OrderStock>> getAllOrderStock()
        {
            return await dal.getAllOrderStock();
        }

        public async Task<OrderStock?> getOrderStockById(int orderId, int stockId)
        {
            return await dal.getOrderStockById(orderId, stockId);
        }
    }
}
