using Dal.IRepositories;
using Dal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Repositories
{
    public class StockDal : IStockDal
    {
        MarketContext db;
        public StockDal(MarketContext db)
        {
            this.db = db;
        }
        public async Task<Stock> AddStock(Stock stock)
        {
            try
            {
                var res = await db.Stocks.AddAsync(stock);
                db.SaveChanges();
                return res.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Stock>> getAllStock()
        {
            try
            {
                return await db.Stocks.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Stock?> getStockById(int id)
        {
            try
            {
                return await db.Stocks
                 .FirstOrDefaultAsync(s => s.ProdId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
