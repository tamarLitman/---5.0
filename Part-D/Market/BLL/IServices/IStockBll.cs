using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IServices
{
    public interface IStockBll
    {
        Task<List<Stock>> getAllStock();
        Task<Stock> getStockById(int id);
        Task<Stock?> AddStock(Stock stock);
    }
}
