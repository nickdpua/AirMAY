using AirMAY.Domain.Models;
using AirMAY.Domain.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AirMAY.Domain.Repository
{
    public class FlightTimeRepository : IRepository<FlightTime>
    {
        private AirMAYDataBaseContext Context { get; }
        public FlightTimeRepository(AirMAYDataBaseContext context) { Context = context; }

        public async Task Add(FlightTime obj)
        {
            await Context.FlightTimes.AddAsync(obj);
            await Context.SaveChangesAsync();
        }

        public async Task Change(FlightTime obj)
        {
            Context.Set<FlightTime>().Update(obj);
            await Context.SaveChangesAsync();          
        }

        public async Task<IReadOnlyCollection<FlightTime>> FindByConditionAsync(Expression<Func<FlightTime, bool>> predicat)
        {
            return await Context.FlightTimes.Where(predicat).Include(x => x.Flight).ToListAsync();
        }

        public async Task<IReadOnlyCollection<FlightTime>> GetAllAsync()
        {
            return await Context.FlightTimes.Include(x=>x.Flight).ToListAsync();
        }

        public async Task Remove(FlightTime obj)
        {
            Context.Remove(await Context.FlightTimes.FirstAsync(x => x.Id == obj.Id));
            await Context.SaveChangesAsync();
        }
    }
}