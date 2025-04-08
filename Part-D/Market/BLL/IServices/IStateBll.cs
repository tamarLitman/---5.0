using Dal.Models;
using DTO.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IServices
{
    public interface IStateBll
    {
        Task<List<StateDto>> getAllStates();
        Task<StateDto?> getStateById(int id);
        Task<StateDto?> AddState(StateDto state);
    }
}
