using AirMAY.Domain.Initializer;
using AirMAY.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirMAY.Domain
{
    public class AirMAYDataBaseContext : DbContext
    {
        public AirMAYDataBaseContext(DbContextOptions<AirMAYDataBaseContext> options) : base(options)
        {
            ModelInitializer.Initialize(this);
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<Sity> Sities { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<TicketUser> TicketUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
    }
}