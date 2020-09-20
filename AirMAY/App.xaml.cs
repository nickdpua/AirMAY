using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using AirMAY.Domain;
using AirMAY.Domain.Repository;
using AirMAY.Services;

namespace AirMAY
{
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            ConfigurationServiceAsync(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();
            ServiceProvider.GetRequiredService<MainWindow>().Show();
        }
        private void ConfigurationServiceAsync(IServiceCollection services)
        {
            services.AddTransient(typeof(MainWindow));
            services.AddTransient(typeof(LoginService));
            services.AddTransient(typeof(FlightService));

            services.AddTransient(typeof(AdminRepository));       
            services.AddTransient(typeof(FlightRepository));
            services.AddTransient(typeof(FlightTimeRepository));
            services.AddTransient(typeof(CityRepository));
            services.AddTransient(typeof(UserRepository));

            services.AddDbContext<AirMAYDataBaseContext>(option => option.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AirMayDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));
        }
    }
}