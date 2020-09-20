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
    public class CityRepository : IRepository<City>
    {
        private AirMAYDataBaseContext Context { get; }
        public CityRepository(AirMAYDataBaseContext context) { Context = context; }

        public async Task Add(City obj)
        {
            await Context.Cities.AddAsync(obj);
            await Context.SaveChangesAsync();
        }

        public async Task Change(City obj)
        {
            Context.Set<City>().Update(obj);
            await Context.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<City>> FindByConditionAsync(Expression<Func<City, bool>> predicat)
        {
            return await Context.Cities.Where(predicat)
                .Include(x => x.FirstSity)
                .Include(x => x.SecondSity).ToListAsync();
        }

        public async Task<IReadOnlyCollection<City>> GetAllAsync()
        {
            return await Context.Cities
                .Include(x => x.FirstSity)
                .Include(x => x.SecondSity).ToListAsync();
        }

        public async Task Remove(City obj)
        {
            Context.Remove(await Context.Cities.FirstAsync(x => x.Id == obj.Id));
            await Context.SaveChangesAsync();
        }
    }
}