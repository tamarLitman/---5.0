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
    public class StockBll : IStockBll
    {
        IStockDal dal;
        IMapper mapper;
        public StockBll(IStockDal dal)
        {
            this.dal = dal;
            var config = new MapperConfiguration(cfg =>
            cfg.AddProfile<MarketProfile>());
            mapper = config.CreateMapper();
        }
        public async Task<StockDto?> AddStock(StockDto stock)
        {
            Stock? newStock= await dal.AddStock(mapper.Map<StockDto,Stock>(stock));
            if(newStock != null)
            {
                return mapper.Map<Stock,StockDto>(newStock);
            }
            return null;
        }

        public async Task<List<StockDto>> getAllStock()
        {
            List<Stock> allProds=await dal.getAllStock();
            return mapper.Map<List<Stock>, List<StockDto>>(allProds);
        }

        public async Task<StockDto?> getStockById(int id)
        {
            Stock? res= await dal.getStockById(id);
            if (res != null)
            {
                return mapper.Map<Stock,StockDto>(res);
            }
            return null;
        }
    }
}
