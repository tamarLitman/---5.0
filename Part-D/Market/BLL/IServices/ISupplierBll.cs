using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IServices
{
    public interface ISupplierBll
    {
        Task<List<Supplier>> getAllSupplier();
        Task<Supplier> getSupplierById(int id);
        Task<Supplier> AddSupplier(Supplier supplier);
    }
}
