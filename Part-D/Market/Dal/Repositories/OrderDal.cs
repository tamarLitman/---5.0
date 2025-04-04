using Dal.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Repositories
{
    public class OrderDal : IOrderDal
    {
        MarketContext db;

        public OrderDal(MarketContext db)
        {
            this.db = db;
        }
        public async Task<Order> AddOrder(Order order)
        {
            try
            {
                //להוסיף לטבלת קישור!!
                var res=await db.Orders.AddAsync(order);
                db.SaveChanges();
                return res.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Order>> getAllOrders()
        {
            try
            {
                return await db.Orders.Include(O => O.OrderState).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Order?> getOrderById(int id)
        {
            try
            {
                return await db.Orders.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
