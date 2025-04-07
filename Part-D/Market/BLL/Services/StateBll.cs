using AutoMapper;
using BLL.IServices;
using Dal.IRepositories;
using Dal.Models;
using DTO.Classes;
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
        IMapper mapper;
        public StateBll(IStateDal dal)
        {
            this.dal = dal;
            var config = new MapperConfiguration(cfg =>
            cfg.AddProfile<MarketProfile>());
            mapper = config.CreateMapper();
        }
        public async Task<StateDto> AddState(StateDto state)
        {
            State newState= await dal.AddState(mapper.Map<StateDto, State>(state));
            if(newState!=null)
            {
                return mapper.Map<State, StateDto>(newState);
            }
            return null;
        }

        public async Task<List<StateDto>> getAllStates()
        {
            List<State> allStates=await dal.getAllStates();
            return mapper.Map<List<State>, List<StateDto>>(allStates);
        }

        public async Task<StateDto?> getStateById(int id)
        {
            State? res = await dal.getStateById(id);
            if(res!=null) {
                return mapper.Map<State,StateDto>(res);
            }
            return null;
        }
    }
}
