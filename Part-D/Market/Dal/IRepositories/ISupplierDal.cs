using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Models;

namespace Dal.IRepositories
{
    public interface ISupplierDal
    {
        Task<List<Supplier>> getAllSuppliers();
        Task<Supplier?> getSupplierById(int id);
        Task<Supplier> AddSupplier(Supplier supplier);
    }
}
