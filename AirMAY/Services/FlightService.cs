using AirMAY.Domain.Models;
using AirMAY.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AirMAY.Services
{
    public class FlightService
    {
        private readonly FlightRepository _flightRepository;

        public FlightService(FlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public async Task<List<Flight>> GetAllFlight()
        {
            return (await _flightRepository.GetAllAsync()).ToList();
        }

        public async Task Change(Flight obj)
        {
           await _flightRepository.Change(obj);
        }

        public async Task<IReadOnlyCollection<Flight>> FindByConditionAsync(Expression<Func<Flight, bool>> predicat)
        {
            return await _flightRepository.FindByConditionAsync(predicat);
        }
    }
}