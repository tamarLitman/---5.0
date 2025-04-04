using BLL.IServices;
using Dal;
using Dal.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class SupplierBll : ISupplierBll
    {
        ISupplierDal dal;
        public SupplierBll(ISupplierDal dal)
        {
            this.dal = dal;
        }
        public async Task<Supplier> AddSupplier(Supplier supplier)
        {
            return await dal.AddSupplier(supplier);
        }

        public async Task<List<Supplier>> getAllSupplier()
        {
            return await dal.getAllSuppliers();
        }

        public async Task<Supplier?> getSupplierById(int id)
        {
            return await dal.getSupplierById(id);
        }
    }
}
