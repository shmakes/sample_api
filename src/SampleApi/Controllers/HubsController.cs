using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleApi.Data;
using SampleApi.Models;
using SampleApi.Services;

namespace SampleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HubsController : ControllerBase
    {
        private readonly HubContext _context;
        private readonly FlightContext _flightContext;
        private readonly HubsService _hubService;

        public HubsController(HubContext context, FlightContext flightContext)
        {
            _context = context;
            _flightContext = flightContext;
            _hubService = new Services.HubsService(_context, _flightContext);
        }

        // GET: api/Hubs
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Hub>>> GetHub()
        {
            return await _hubService.GetHubs();
        }

        // GET: api/Hubs/5
        [ApiVersion("1.0")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Hub), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Hub>> GetHub(int id)
        {
            var hub = await _context.Hub.Include(hub => hub.Flights).SingleOrDefaultAsync(hub => hub.Id == id);

            if (hub == null)
            {
                return NotFound();
            }

            return hub;
        }

        // PUT: api/Hubs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutHub(int id, Hub hub)
        {
            if (id != hub.Id)
            {
                return BadRequest();
            }

            _context.Entry(hub).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HubExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Hubs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(typeof(Hub), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Hub>> PostHub(Hub hub)
        {
            _context.Hub.Add(hub);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHub", new { id = hub.Id }, hub);
        }

        // DELETE: api/Hubs/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(object), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteHub(int id)
        {
            var hub = await _context.Hub.Include(hub => hub.Flights).SingleOrDefaultAsync(hub => hub.Id == id);
            if (hub == null)
            {
                return NotFound();
            }

/*             foreach(var flight in hub.Flights) {
                _flightContext.Flight.Remove(flight);
            }
            await _flightContext.SaveChangesAsync() */;

            _context.Hub.Remove(hub);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HubExists(int id)
        {
            return _context.Hub.Any(e => e.Id == id);
        }
    }
}
