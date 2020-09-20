using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirMAY.Domain.Initializer
{
    public static class ModelInitializer
    {
        public static void Initialize(AirMAYDataBaseContext context)
        {
            context.Database.EnsureCreated();

            if (context.Flights.Any()) return;

            // Add Cities
            context.Cities.Add(new Models.City() { SityName = "Киев" });
            context.Cities.Add(new Models.City() { SityName = "Днепр" });
            context.Cities.Add(new Models.City() { SityName = "Запорожье" });
            context.Cities.Add(new Models.City() { SityName = "Харьков" });
            context.Cities.Add(new Models.City() { SityName = "Одесса" });
            context.Cities.Add(new Models.City() { SityName = "Львов" });

            context.SaveChanges();

            // Add Flights
            context.Flights.Add(new Models.Flight()
            {
                FirstSityId = context.Cities.First(x => x.SityName == "Киев").Id,
                SecondSityId = context.Cities.First(x => x.SityName == "Одесса").Id,
                Price = 30,
            });
            context.SaveChanges();

            //Add FlightTimes
            context.FlightTimes.Add(new Models.FlightTime()
            {
                FlightId = context.Flights.First(x => x.FirstSity.SityName == "Киев" && x.SecondSity.SityName == "Одесса").Id,
                TimeOfDispatch = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Minute,
                14, 00, 00),
                EstimatedArrivalTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Minute,
                15, 00, 00),
            });

            context.FlightTimes.Add(new Models.FlightTime()
            {
                FlightId = context.Flights.First(x => x.FirstSity.SityName == "Киев" && x.SecondSity.SityName == "Одесса").Id,
                TimeOfDispatch = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Minute,
                18, 00, 00),
                EstimatedArrivalTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Minute,
                19, 00, 00),
            });
            context.SaveChanges();

            context.Users.Add(new Models.User() { Login = "Admin", Password = "12345", Name = "Serega", Surname = "Kurapatkov" });
            context.SaveChanges();

        }
    }
}