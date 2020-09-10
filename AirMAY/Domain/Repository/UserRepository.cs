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
            var user = (await Context.Users.FirstOrDefaultAsync(x => x.Id == obj.Id));
            if (user != null)
            {
                user.Name = obj.Name;
                user.Surname = obj.Surname;
                user.Login = obj.Login;
                user.Password = obj.Password;
                user.Email = obj.Email;

                await Context.SaveChangesAsync();
            }
        }

        public async Task<IReadOnlyCollection<User>> FindByConditionAsync(Expression<Func<User, bool>> predicat)
        {
            return await Context.Users.Where(predicat).ToListAsync();
        }

        public async Task<IReadOnlyCollection<User>> GetAllAsync()
        {
            return await Context.Users.ToListAsync();
        }
    }
}