using BLL.IServices;
using Dal.Models;
using DTO.Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        IStockBll bll;
        public StockController(IStockBll bll)
        {
            this.bll = bll;
        }
        [HttpGet]
        public async Task<List<StockDto>> getAll()
        {
            return await bll.getAllStock();
        }
        [HttpGet("{id}")]
        public async Task<StockDto> getById(int id)
        {
            return await bll.getStockById(id);
        }
        [HttpPost]
        public async Task<StockDto?> Add([FromBody] StockDto obj)
        {
            return await bll.AddStock(obj);
        }
    }
}
