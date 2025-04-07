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
    public class SupplierDal : ISupplierDal
    {
        MarketContext db;
        public SupplierDal(MarketContext db)
        {
            this.db = db;
        }

        public async Task<Supplier> AddSupplier(Supplier supplier)
        {
            try
            {
                var res = await db.Suppliers.AddAsync(supplier);
                db.SaveChanges();
                return res.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Supplier>> getAllSuppliers()
        {
            try
            {
                return await db.Suppliers.Include(s=>s.Stocks).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Supplier?> getSupplierById(int id)
        {
            try
            {
                return await db.Suppliers.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
