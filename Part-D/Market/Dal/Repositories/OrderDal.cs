using Dal.IRepositories;
using Dal.Models;
using Microsoft.EntityFrameworkCore;


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
                db.ChangeTracker.Clear();
                var res = await db.Orders.AddAsync(order);
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
                return await db.Orders.Include(O => O.OrderState).Include(O=>O.Prods).ToListAsync();
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

    public async Task<bool> UpdateOrder(Order order)
    {
      try
       {
        Order? o = await db.Orders.FindAsync(order.OrderId);
        if (o != null)
        {
          o.OrderStateId = order.OrderStateId;
          o.OrderState = await db.States.FindAsync(order.OrderStateId);
          if (o.OrderState != null)
          {
            Console.WriteLine("Before SaveChangesAsync");
            await db.SaveChangesAsync();
            Console.WriteLine("After SaveChangesAsync");
            return true;
          }
        }
        return false;
      }
      catch(Exception ex)
      {
        return false;
      }
    }
  }
}
