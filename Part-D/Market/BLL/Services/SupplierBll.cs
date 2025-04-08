using AutoMapper;
using BLL.IServices;
using Dal.IRepositories;
using Dal.Models;
using DTO.Classes;
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
        IMapper mapper;
        public SupplierBll(ISupplierDal dal)
        {
            this.dal = dal;
            var config = new MapperConfiguration(cfg =>
            cfg.AddProfile<MarketProfile>());
            mapper = config.CreateMapper();
        }
        public async Task<SupplierDto?> AddSupplier(SupplierDto supplier)
        {
            Supplier newSupplier= await dal.AddSupplier(mapper.Map<SupplierDto,Supplier>(supplier));
            if(newSupplier != null)
            {
                return mapper.Map<Supplier, SupplierDto>(newSupplier);
            }
            return null;
        }

        public async Task<List<SupplierDto>> getAllSupplier()
        {
            List<Supplier> allSuppliers= await dal.getAllSuppliers();
            return mapper.Map<List<Supplier>, List<SupplierDto>>(allSuppliers);
        }

        public async Task<SupplierDto?> getSupplierById(int id)
        {
            Supplier? res= await dal.getSupplierById(id);
            if(res != null)
            {
                return mapper.Map<Supplier, SupplierDto>(res);
            }
            return null;
        }
    }
}
