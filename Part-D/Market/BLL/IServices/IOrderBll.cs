using Dal;
using Dal.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IServices
{
    public interface IOrderBll
    {
        Task<List<Order>> getAllOrders();
        Task<Order> getOrderById(int id);
        Task<Order> AddOrder(Order order);
    }
}
