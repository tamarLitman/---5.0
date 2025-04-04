using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IServices
{
    public interface IStateBll
    {
        Task<List<State>> getAllStates();
        Task<State> getStateById(int id);
        Task<State> AddState(State state);
    }
}
