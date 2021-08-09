using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleApi;

    public class HubContext : DbContext
    {
        public HubContext (DbContextOptions<HubContext> options)
            : base(options)
        {
        }

        public DbSet<SampleApi.Hub> Hub { get; set; }
    }
