using BLL.IServices;
using Dal;
using Dal.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class StateBll : IStateBll
    {
        IStateDal dal;
        public StateBll(IStateDal dal)
        {
            this.dal = dal;
        }
        public async Task<State> AddState(State state)
        {
            return await dal.AddState(state);
        }

        public async Task<List<State>> getAllStates()
        {
            return await dal.getAllStates();
        }

        public async Task<State?> getStateById(int id)
        {
            return await dal.getStateById(id);
        }
    }
}
