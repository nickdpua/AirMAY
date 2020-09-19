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
    public class AdminRepository : IRepository<Admin>
    {
        private AirMAYDataBaseContext Context { get; }
        public AdminRepository(AirMAYDataBaseContext context) { Context = context; }

        public async Task Add(Admin obj)
        {
            await Context.Admins.AddAsync(obj);
            await Context.SaveChangesAsync();
        }

        public async Task Change(Admin obj)
        {
            Context.Set<Admin>().Update(obj);
            await Context.SaveChangesAsync();           
        }

        public async Task<IReadOnlyCollection<Admin>> FindByConditionAsync(Expression<Func<Admin, bool>> predicat)
        {
            return await Context.Admins.Where(predicat).ToListAsync();
        }

        public async Task<IReadOnlyCollection<Admin>> GetAllAsync()
        {
            return await Context.Admins.ToListAsync();
        }

        public async Task Remove(Admin obj)
        {
            Context.Remove(await Context.Admins.FirstAsync(x => x.Id == obj.Id));
            await Context.SaveChangesAsync();
        }
    }
}