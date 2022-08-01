using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using si.ineor.webapi.Models;

    public class ineorwebapiContext : DbContext
    {
        public ineorwebapiContext (DbContextOptions<ineorwebapiContext> options)
            : base(options)
        {
        }

        public DbSet<si.ineor.webapi.Models.User> User { get; set; } = default!;

        public DbSet<si.ineor.webapi.Models.Movie>? Movie { get; set; }

        public DbSet<si.ineor.webapi.Models.Rental>? Rental { get; set; }
    }
