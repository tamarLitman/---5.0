using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Models;

namespace Dal.IRepositories
{
    public interface IOrderDal
    {
        Task<List<Order>> getAllOrders();
        Task<Order?> getOrderById(int id);
        Task<Order> AddOrder(Order order);
        Task<bool> UpdateOrder(Order order);
    }
}
