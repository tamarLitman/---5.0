using BLL.IServices;
using Dal.Models;
using DTO.Classes;
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
        public async Task<List<OrderDto>> getAll()
        {
            return await bll.getAllOrders();
        }
        [HttpGet("{id}")]
        public async Task<OrderDto?> getById(int id)
        {
            return await bll.getOrderById(id);
        }
        [HttpPost]
        public async Task<OrderDto?> Add([FromBody] OrderDto obj)
        {
            return await bll.AddOrder(obj);
        }
        [HttpPut]

        public async Task<bool> UpdateTrip([FromBody] OrderDto order)
        {
          return await bll.UpdateOrder(order);
        }
  }
}
