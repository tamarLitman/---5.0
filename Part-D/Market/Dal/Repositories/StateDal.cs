using Dal.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Repositories
{
    public class StateDal : IStateDal
    {
        MarketContext db;
        public StateDal(MarketContext db)
        {
            this.db = db; 
        }
        public async Task<State> AddState(State state)
        {
            try
            {
                var res = await db.AddAsync(state);
                db.SaveChanges();
                return res.Entity;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<State>> getAllStates()
        {
            try
            {
                return await db.States.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<State?> getStateById(int id)
        {
            try
            {
                return await db.States.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
