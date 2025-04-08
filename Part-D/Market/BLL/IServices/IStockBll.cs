using Dal.Models;
using DTO.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IServices
{
    public interface IStockBll
    {
        Task<List<StockDto>> getAllStock();
        Task<StockDto?> getStockById(int id);
        Task<StockDto?> AddStock(StockDto stock);
    }
}
