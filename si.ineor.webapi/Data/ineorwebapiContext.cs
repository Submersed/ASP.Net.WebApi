using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using si.ineor.webapi.Entities;
using si.ineor.webapi.Models;

public class IneorwebapiContext : DbContext
    {
        public IneorwebapiContext (DbContextOptions<IneorwebapiContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; } = default!;

        public DbSet<Movie>? Movie { get; set; }

        public DbSet<Rental>? Rental { get; set; }

        private readonly IConfiguration Configuration;

        public IneorwebapiContext()
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
        }
}
