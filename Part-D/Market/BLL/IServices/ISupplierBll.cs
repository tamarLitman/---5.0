using Dal.Models;
using DTO.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IServices
{
    public interface ISupplierBll
    {
        Task<List<SupplierDto>> getAllSupplier();
        Task<SupplierDto?> getSupplierById(int id);
        Task<SupplierDto?> AddSupplier(SupplierDto supplier);
    }
}
