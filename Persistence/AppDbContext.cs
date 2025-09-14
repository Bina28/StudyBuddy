using System;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Persistence;

public class AppDbContext(DbContextOptions options) : IdentityDbContext<User>(options)
{
  public required DbSet<Activity> Activities { get; set; }
  public required DbSet<ActivityAtendee> ActivtyAtendees { get; set; }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);

    builder.Entity<ActivityAtendee>(x => x.HasKey(a => new { a.ActivityId, a.UserId }));

    builder.Entity<ActivityAtendee>()
    .HasOne(x => x.User)
    .WithMany(x => x.Activities)
    .HasForeignKey(x => x.UserId);

    builder.Entity<ActivityAtendee>()
    .HasOne(x => x.Activity)
    .WithMany(x => x.Attendees)
    .HasForeignKey(x => x.ActivityId);
  }

}
