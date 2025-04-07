using BLL.IServices;
using Dal.Models;
using DTO.Classes;
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
        public async Task<List<StateDto>> getAll()
        {
            return await bll.getAllStates();
        }
        [HttpGet("{id}")]
        public async Task<StateDto?> getById(int id)
        {
            return await bll.getStateById(id);
        }
        [HttpPost]
        public async Task<StateDto?> Add([FromBody] StateDto obj)
        {
            return await bll.AddState(obj);
        }
    }
}
