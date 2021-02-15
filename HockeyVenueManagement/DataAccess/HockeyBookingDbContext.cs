using HockeyVenueManagement.Model;
using HockeyVenueManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace HockeyVenueManagement.DataAccess
{
    public class HockeyBookingDbContext : IdentityDbContext<HockeyBookingsUser, HockeyBookingsRole, string>
    {
        public HockeyBookingDbContext(DbContextOptions<HockeyBookingDbContext> options)
            : base(options)
        {
        }

        public DbSet<Pitch> Pitches { get; set; }

        public DbSet<PitchBooking> PitchBookings { get; set; }

        public DbSet<Member> Members { get; set; }
        
        public DbSet<PitchMaintenanceSchedule> PitchMaintenance { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>()
                .HasOne(x => x.User)
                .WithOne(x => x.Member)
                .HasForeignKey<Member>(x => x.UserId);

            modelBuilder.Entity<Pitch>().HasData(
                new Pitch { Id = 1, Name = "Pitch 1", Type = PitchType.grass },
                new Pitch { Id = 2, Name = "Pitch 2", Type = PitchType.grass },
                new Pitch { Id = 3, Name = "Pitch 3", Type = PitchType.synthetic },
                new Pitch { Id = 4, Name = "Pitch 4", Type = PitchType.synthetic},
                new Pitch { Id = 5, Name = "Pitch 5", Type = PitchType.synthetic });
         
            modelBuilder.Entity<PitchMaintenanceSchedule>().HasData(
                new PitchMaintenanceSchedule
                {
                    Id = 1,
                    WorkTitle = "Resurface",
                    PitchIsClosed = true,
                    StartDate = new DateTime(2019, 03, 01, 06, 00, 00),
                    EndDate = new DateTime(2019, 03, 07, 22, 00, 00),
                    PitchId = 4
                },
                new PitchMaintenanceSchedule
                {
                    Id = 2,
                    WorkTitle = "Available",
                    PitchIsClosed = false,
                    StartDate = new DateTime(2019, 04, 15, 12, 00, 00),
                    EndDate = new DateTime(2019, 04, 15, 13, 15, 00),
                    PitchId = 1
                },
                new PitchMaintenanceSchedule
                {
                    Id = 3,
                    WorkTitle = "Water logged",
                    PitchIsClosed = true,
                    StartDate = new DateTime(2019, 02, 08, 07, 00, 00),
                    EndDate = new DateTime(2019, 02, 08, 09, 00, 00),
                    PitchId = 2
                });
       
            base.OnModelCreating(modelBuilder);
        }
    }
}
