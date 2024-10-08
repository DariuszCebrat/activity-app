﻿using activity.domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace activity.infrastructure
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<ActivityAttendee> ActivityAttendees { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Photo> Photos { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ActivityAttendee>(x => x.HasKey(aa => new {aa.ActivityId,aa.AppUserId }));
            builder.Entity<ActivityAttendee>()
                .HasOne(u=>u.AppUser)
                .WithMany(a=>a.Activities)
                .HasForeignKey(aa=>aa.AppUserId);

            builder.Entity<ActivityAttendee>()
              .HasOne(u => u.Activity)
              .WithMany(a => a.Attendies)
              .HasForeignKey(aa => aa.ActivityId);
        }
    }
}
