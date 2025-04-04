using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IServices
{
    public interface IOrderStockBll
    {
        Task<List<OrderStock>> getAllOrderStock();
        Task<OrderStock?> getOrderStockById(int orderId,int stockId);
        Task<OrderStock> AddOrderStock(OrderStock orderStock);
    }
}
