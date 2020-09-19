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
    public class UserRepository : IRepository<User>
    {
        private AirMAYDataBaseContext Context { get; }
        public UserRepository(AirMAYDataBaseContext context) { Context = context; }

        public async Task Add(User obj)
        {
            await Context.Users.AddAsync(obj);
            await Context.SaveChangesAsync();
        }

        public async Task Change(User obj)
        {
            Context.Set<User>().Update(obj);
            await Context.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<User>> FindByConditionAsync(Expression<Func<User, bool>> predicat)
        {
            return await Context.Users.Where(predicat).Include(x=>x.FlightUser).ToListAsync();
        }

        public async Task<IReadOnlyCollection<User>> GetAllAsync()
        {
            return await Context.Users.Include(x => x.FlightUser).ToListAsync();
        }

        public async Task Remove(User obj)
        {
            Context.Remove(await Context.Users.FirstAsync(x => x.Id == obj.Id));
            await Context.SaveChangesAsync();
        }
    }
}