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
    public class SityRepository : IRepository<Sity>
    {
        private AirMAYDataBaseContext Context { get; }
        public SityRepository(AirMAYDataBaseContext context) { Context = context; }

        public async Task Add(Sity obj)
        {
            await Context.Sities.AddAsync(obj);
            await Context.SaveChangesAsync();
        }

        public async Task Change(Sity obj)
        {
            Context.Set<Sity>().Update(obj);
            await Context.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<Sity>> FindByConditionAsync(Expression<Func<Sity, bool>> predicat)
        {
            return await Context.Sities.Where(predicat)
                .Include(x=>x.Hotels)
                .Include(x => x.FirstSity)
                .Include(x => x.SecondSity).ToListAsync();
        }

        public async Task<IReadOnlyCollection<Sity>> GetAllAsync()
        {
            return await Context.Sities
                .Include(x => x.FirstSity)
                .Include(x => x.SecondSity).ToListAsync();
        }

        public async Task Remove(Sity obj)
        {
            Context.Remove(await Context.Sities.FirstAsync(x => x.Id == obj.Id));
            await Context.SaveChangesAsync();
        }
    }
}