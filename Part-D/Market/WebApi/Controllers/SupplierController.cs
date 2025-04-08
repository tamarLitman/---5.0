using BLL.IServices;
using Dal.Models;
using DTO.Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        ISupplierBll bll;
        public SupplierController(ISupplierBll bll)
        {
            this.bll = bll;
        }
        [HttpGet]
        public async Task<List<SupplierDto>> getAll()
        {
            return await bll.getAllSupplier();
        }
        [HttpGet("{id}")]
        public async Task<SupplierDto> getById(int id)
        {
            return await bll.getSupplierById(id);
        }
        [HttpPost]
        public async Task<SupplierDto> Add([FromBody] SupplierDto obj)
        {
            return await bll.AddSupplier(obj);
        }
    }
}
