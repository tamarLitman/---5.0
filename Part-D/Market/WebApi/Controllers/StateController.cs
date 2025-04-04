using BLL.IServices;
using Dal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        IStateBll bll;
        public StateController(IStateBll bll)
        {
            this.bll = bll;
        }
        [HttpGet]
        public async Task<List<State>> getAll()
        {
            return await bll.getAllStates();
        }
        [HttpGet("{id}")]
        public async Task<State> getById(int id)
        {
            return await bll.getStateById(id);
        }
        [HttpPost]
        public async Task<State> Add([FromBody] State obj)
        {
            return await bll.AddState(obj);
        }
    }
}
