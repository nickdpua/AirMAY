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
            Database.EnsureCreated();
            ModelInitializer.Initialize(this);
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<FlightTime> FlightTimes { get; set; }
        public virtual DbSet<FlightUser> FlightUsers { get; set; }
        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<Sity> Sities { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Sity>()
                .HasMany(x => x.Hotels)
                .WithOne(x => x.Sity)
                .HasForeignKey(x => x.SityId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Sity>()
                .HasMany(x => x.Flights)
                .WithOne(x => x.FirstSity)
                .HasForeignKey(x => x.FirstSityId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Sity>()
               .HasMany(x => x.Flights)
               .WithOne(x => x.SecondSity)
               .HasForeignKey(x => x.SecondSityId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Flight>()
                .HasMany(x => x.FlightTimes)
                .WithOne(x => x.Flight)
                .HasForeignKey(x => x.FlightId).OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<FlightUser>().HasKey(x => new { x.FlightId, x.UserId });
            modelBuilder.Entity<FlightUser>()
               .HasOne<Flight>(x => x.Flight)
               .WithMany(x => x.FlightUser)
               .HasForeignKey(x => x.FlightId);

            modelBuilder.Entity<FlightUser>()
                .HasOne<User>(x => x.User)
                .WithMany(x => x.FlightUser)
                .HasForeignKey(x => x.UserId);


            //base.OnModelCreating(modelBuilder);
        }
    }
}