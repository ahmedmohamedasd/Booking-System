using BackEnd.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Data
{
    public class ApplicationDbContext :IdentityDbContext
    {
        public DbSet<Area> Areas { get; set; }
        public DbSet<BlockArea> BlockAreas { get; set; }
        public DbSet<WhereYouFrom> WhereYouFroms { get; set; }
        public DbSet<HowYouKnowUs> HowYouKnowUss { get; set; }
        public DbSet<DepositWay> DepositWays { get; set; }
        public DbSet<BookingType> BookingTypes { get; set; }
        public DbSet<Activity> Activitys { get; set; }
        public DbSet<Ticket> tickets { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<GuestActivity> GuestActivities { get; set; }
        public DbSet<GuestTicket> GuestTickets { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<GuestEvent> GuestEvents { get; set; }
        public DbSet<BookedGuestArea> BookedGuestAreas { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Many to Many Guest , Area in BookedGuestArea
            modelBuilder.Entity<BookedGuestArea>().HasKey(cc => new { cc.GuestId, cc.AreaId });
            modelBuilder.Entity<BookedGuestArea>().HasOne(c => c.Guest)
                                                  .WithMany(c => c.BookedGuestAreas)
                                                  .HasForeignKey(c => c.GuestId);
            modelBuilder.Entity<BookedGuestArea>().HasOne(cc => cc.Area)
                                                  .WithMany(c => c.BookedGuestAreas)
                                                  .HasForeignKey(c => c.AreaId);
            // Many to Many Guest , Activity in GuestActivity
            modelBuilder.Entity<GuestActivity>().HasKey(c => new { c.ActivityId, c.GuestId });
            modelBuilder.Entity<GuestActivity>().HasOne(c => c.Guest)
                                                .WithMany(c => c.GuestActivities)
                                                .HasForeignKey(c => c.GuestId);
            modelBuilder.Entity<GuestActivity>().HasOne(c => c.Activity)
                                                .WithMany(c => c.GuestActivities)
                                                .HasForeignKey(c => c.ActivityId);

            // Many to Many Guest , Activity in GuestActivity
            modelBuilder.Entity<GuestTicket>().HasKey(c => new { c.GuestId, c.TicketId });
            modelBuilder.Entity<GuestTicket>().HasOne(c => c.Guest)
                                              .WithMany(c => c.GuestTickets)
                                              .HasForeignKey(c => c.GuestId);
            modelBuilder.Entity<GuestTicket>().HasOne(c => c.Ticket)
                                              .WithMany(c => c.GuestTickets)
                                              .HasForeignKey(c => c.TicketId);

        }
    }

}
