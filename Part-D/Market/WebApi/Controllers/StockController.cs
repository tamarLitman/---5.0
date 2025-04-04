using BLL.IServices;
using Dal;
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
        public async Task<List<Stock>> getAll()
        {
            return await bll.getAllStock();
        }
        [HttpGet("{id}")]
        public async Task<Stock> getById(int id)
        {
            return await bll.getStockById(id);
        }
        [HttpPost]
        public async Task<Stock?> Add([FromBody] Stock obj)
        {
            return await bll.AddStock(obj);
        }
    }
}
