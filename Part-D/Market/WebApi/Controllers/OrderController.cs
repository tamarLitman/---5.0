using BLL.IServices;
using Dal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IOrderBll bll;
        public OrderController(IOrderBll bll)
        {
            this.bll = bll;
        }
        [HttpGet]
        public async Task<List<Order>> getAll()
        {
            return await bll.getAllOrders();
        }
        [HttpGet("{id}")]
        public async Task<Order> getById(int id)
        {
            return await bll.getOrderById(id);
        }
        [HttpPost]
        public async Task<Order> Add([FromBody] Order obj)
        {
            return await bll.AddOrder(obj);
        }
    }
}
