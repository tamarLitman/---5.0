using BLL.IServices;
using Dal;
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
        public async Task<List<Supplier>> getAll()
        {
            return await bll.getAllSupplier();
        }
        [HttpGet("{id}")]
        public async Task<Supplier> getById(int id)
        {
            return await bll.getSupplierById(id);
        }
        [HttpPost]
        public async Task<Supplier> Add([FromBody] Supplier obj)
        {
            return await bll.AddSupplier(obj);
        }
    }
}
