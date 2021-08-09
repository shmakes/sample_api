using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleApi;

    public class FlightContext : DbContext
    {
        public FlightContext (DbContextOptions<FlightContext> options)
            : base(options)
        {
        }

        public DbSet<SampleApi.Flight> Flight { get; set; }
    }
