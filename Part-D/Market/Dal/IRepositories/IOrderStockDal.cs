using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.IRepositories
{
    public interface IOrderStockDal
    {
        Task<List<OrderStock>> getAllOrderStock();
        Task<OrderStock?> getOrderStockById(int orderId,int stockId);
        Task<OrderStock> AddOrderStock(OrderStock order);
    }
}
