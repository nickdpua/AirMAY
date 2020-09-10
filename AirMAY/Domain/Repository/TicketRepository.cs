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
    public class TicketRepository : IRepository<Ticket>
    {
        private AirMAYDataBaseContext Context { get; }
        public TicketRepository(AirMAYDataBaseContext context) { Context = context; }

        public async Task Add(Ticket obj)
        {
            await Context.Tickets.AddAsync(obj);
            await Context.SaveChangesAsync();
        }

        public Task Change(Ticket obj)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyCollection<Ticket>> FindByConditionAsync(Expression<Func<Ticket, bool>> predicat)
        {
            return await Context.Tickets.Where(predicat).ToListAsync();
        }

        public async Task<IReadOnlyCollection<Ticket>> GetAllAsync()
        {
            return await Context.Tickets.ToListAsync();
        }
    }
}
