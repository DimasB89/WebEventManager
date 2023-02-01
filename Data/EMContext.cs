using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebEventManager.Models;

namespace WebEventManager.Data
{
    public class EMContext : DbContext
    {
        public EMContext (DbContextOptions<EMContext> options)
            : base(options)
        {
        }

        public DbSet<WebEventManager.Models.Participant> Participants { get; set; } = default!;
        public DbSet<WebEventManager.Models.Attendance> Attendances { get; set; } = default!;
        public DbSet<WebEventManager.Models.Event> Events { get; set; } = default!;
        public DbSet<Company> Companies { get; set; } = default!;
        public DbSet<PrivatePerson> PrivatePersons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().ToTable("Event");
            modelBuilder.Entity<Attendance>().ToTable("Attendance");
            modelBuilder.Entity<Participant>().ToTable("Participant");
            modelBuilder.Entity<Company>().ToTable("Company");
            modelBuilder.Entity<PrivatePerson>().ToTable("PrivatePerson");
        }
    }
}
