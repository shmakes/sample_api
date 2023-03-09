using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleApi.Models;

namespace SampleApi.Data
{
    public class HubContext : DbContext
    {
        public HubContext(DbContextOptions<HubContext> options)
            : base(options)
        {
        }

        public DbSet<Hub> Hub { get; set; }
    }
}