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
    public class HotelRepository : IRepository<Hotel>
    {
        private AirMAYDataBaseContext Context { get; }
        public HotelRepository(AirMAYDataBaseContext context) { Context = context; }

        public async Task Add(Hotel obj)
        {
            await Context.Hotels.AddAsync(obj);
            await Context.SaveChangesAsync();
        }

        public async Task Change(Hotel obj)
        {
            var hotel = (await Context.Hotels.FirstOrDefaultAsync(x => x.Id == obj.Id));
            if (hotel != null)
            {
                await Context.SaveChangesAsync();
            }
        }

        public async Task<IReadOnlyCollection<Hotel>> FindByConditionAsync(Expression<Func<Hotel, bool>> predicat)
        {
            return await Context.Hotels.Where(predicat).Include(x=>x.Sity).ToListAsync();
        }

        public async Task<IReadOnlyCollection<Hotel>> GetAllAsync()
        {
            return await Context.Hotels.Include(x => x.Sity).ToListAsync();
        }
    }
}
