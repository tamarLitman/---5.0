using Dal.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Repositories
{
    public class OrderStockDal : IOrderStockDal
    {
        MarketContext db;

        public OrderStockDal(MarketContext db)
        {
            this.db = db;
        }
        public async Task<OrderStock> AddOrderStock(OrderStock orderStock)
        {
            try
            {
                await db.OrderStocks.AddAsync(orderStock);
                db.SaveChanges();
                return orderStock;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<OrderStock>> getAllOrderStock()
        {
            try
            {
                return await db.OrderStocks.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<OrderStock?> getOrderStockById(int orderId, int stockId)
        {
            try
            {
                return await db.OrderStocks.FindAsync(orderId,stockId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
