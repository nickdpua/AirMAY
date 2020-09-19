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
    public class FlightRepository : IRepository<Flight>
    {
        private AirMAYDataBaseContext Context { get; }
        public FlightRepository(AirMAYDataBaseContext context) { Context = context; }

        public async Task Add(Flight obj)
        {
            await Context.Flights.AddAsync(obj);
            await Context.SaveChangesAsync();
        }

        public async Task Change(Flight obj)
        {
            Context.Set<Flight>().Update(obj);
            await Context.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<Flight>> FindByConditionAsync(Expression<Func<Flight, bool>> predicat)
        {
            return await Context.Flights.Where(predicat)
                .Include(x=>x.FirstSity)
                .Include(x => x.SecondSity)
                .Include(x => x.FlightUser)
                .Include(x => x.FlightTimes).ToListAsync();
        }

        public async Task<IReadOnlyCollection<Flight>> GetAllAsync()
        {
            return await Context.Flights
                .Include(x => x.FirstSity)
                .Include(x => x.SecondSity)
                .Include(x => x.FlightUser)
                .Include(x => x.FlightTimes).ToListAsync();
        }

        public async Task Remove(Flight obj)
        {
            Context.Remove(await Context.Flights.FirstAsync(x => x.Id == obj.Id));
            await Context.SaveChangesAsync();
        }
    }
}
