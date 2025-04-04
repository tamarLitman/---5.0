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
    public class StockBll : IStockBll
    {
        IStockDal dal;
        public StockBll(IStockDal dal)
        {
            this.dal=dal;
        }
        public async Task<Stock> AddStock(Stock stock)
        {
            return await dal.AddStock(stock);
        }

        public async Task<List<Stock>> getAllStock()
        {
            return await dal.getAllStock();
        }

        public async Task<Stock?> getStockById(int id)
        {
            return await dal.getStockById(id);
        }
    }
}
