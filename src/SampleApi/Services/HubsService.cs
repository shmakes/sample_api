using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleApi.Data;
using SampleApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleApi.Services
{
    public class HubsService
    {
        private readonly HubContext _context;
        private readonly FlightContext _flightContext;

        public HubsService(HubContext context, FlightContext flightContext)
        {
            _context = context;
            _flightContext = flightContext;
        }

        public async Task<ActionResult<IEnumerable<Hub>>> GetHubs()
        {
            return await _context.Hub.Include(hub => hub.Flights).ToListAsync();
        }

    }
}
