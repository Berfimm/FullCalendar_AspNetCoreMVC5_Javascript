using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using W2UIFullCalendarProject.Models;

namespace W2UIFullCalendarProject.Data
{
    public class Context : DbContext
    {
        public Context()
        {

        }
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<RDWorklogTracking> RDWorklogTracking { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         

        }
    }

}
