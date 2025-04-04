using BLL.IServices;
using Dal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStockController : ControllerBase
    {
        IOrderStockBll bll;
        public OrderStockController(IOrderStockBll bll)
        {
            this.bll = bll;
        }
        [HttpGet]
        public async Task<List<OrderStock>> getAll()
        {
            return await bll.getAllOrderStock();
        }
        [HttpGet("{orderId}/{stockId}")]
        public async Task<OrderStock?> getById(int orderId,int stockId)
        {
            return await bll.getOrderStockById(orderId,stockId);
        }
        [HttpPost]
        public async Task<OrderStock> Add([FromBody] OrderStock obj)
        {
            return await bll.AddOrderStock(obj);
        }
    }
}
