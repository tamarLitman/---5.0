using Dal.Models;
using DTO.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IServices
{
    public interface IOrderBll
    {
        Task<List<OrderDto>> getAllOrders();
        Task<OrderDto?> getOrderById(int id);
        Task<OrderDto?> AddOrder(OrderDto order);
        Task<bool> UpdateOrder(OrderDto order);
  }
}
