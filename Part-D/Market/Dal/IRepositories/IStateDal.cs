using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.IRepositories
{
    public interface IStateDal
    {
        Task<List<State>> getAllStates();
        Task<State?> getStateById(int id);
        Task<State> AddState(State state);
    }
}
