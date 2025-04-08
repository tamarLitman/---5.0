using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Models;

namespace Dal.IRepositories
{
    public interface IStockDal
    {
        Task<List<Stock>> getAllStock();
        Task<Stock?> getStockById(int id);
        Task<Stock> AddStock(Stock stock);
    }
}
