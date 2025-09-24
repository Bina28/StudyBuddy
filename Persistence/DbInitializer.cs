using System;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence;

public class DbInitializer
{
    public static async Task SeedData(AppDbContext context, UserManager<User> userManager)
    {
        var users = new List<User>
        {
        new() {Id="bob-id", DisplayName="Bob", UserName="bob@gmail.com", Email="bob@gmail.com"},
        new() {Id="tom-id", DisplayName="Tom", UserName="tom@gmail.com", Email="tom@gmail.com"},
        new() {Id="jane-id", DisplayName="Jane", UserName="jane@gmail.com", Email="jane@gmail.com"}
        };
        if (!userManager.Users.Any())
        {
            foreach (var user in users)
            {
                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }

        if (context.Activities.Any()) return;
        var activities = new List<Activity>
{
    new()
    {
        Title = "Kaffemøte på campus",
        Date = DateTime.Now.AddMonths(-2),
        Description = "Studentene møtes for å diskutere fag over kaffe",
        Category = "cafeMeeting",
        City = "Oslo",
        LocationName = "Universitetet i Oslo, Blindern, Oslo, Norge",
        Latitude = 59.9410,
        Longitude = 10.7200,
        University = "Universitetet i Oslo",
        Attendees =
        [
            new() { UserId = users[0].Id, IsHost = true },
            new() { UserId = users[1].Id }
        ]
    },
    new()
    {
        Title = "Språkgruppe",
        Date = DateTime.Now.AddMonths(-1),
        Description = "Øve på fremmedspråk sammen med andre studenter",
        Category = "language",
        City = "Bergen",
        LocationName = "Universitetet i Bergen, Bergen, Norge",
        Latitude = 60.3913,
        Longitude = 5.3221,
        University = "Universitetet i Bergen",
        Attendees =
        [
            new() { UserId = users[1].Id, IsHost = true },
            new() { UserId = users[2].Id },
            new() { UserId = users[0].Id }
        ]
    },
    new()
    {
        Title = "Matematikkgruppe",
        Date = DateTime.Now.AddMonths(1),
        Description = "Løse matteoppgaver sammen",
        Category = "mathGroup",
        City = "Trondheim",
        LocationName = "NTNU, Trondheim, Norge",
        Latitude = 63.4172,
        Longitude = 10.4023,
        University = "NTNU",
        Attendees =
        [
            new() { UserId = users[2].Id, IsHost = true }
        ]
    },
    new()
    {
        Title = "Programmeringsgruppe",
        Date = DateTime.Now.AddMonths(2),
        Description = "Kode sammen og løse programmeringsoppgaver",
        Category = "programmingGroup",
        City = "Stavanger",
        LocationName = "Universitetet i Stavanger, Stavanger, Norge",
        Latitude = 58.9680,
        Longitude = 5.7331,
        University = "Universitetet i Stavanger",
        Attendees =
        [
            new() { UserId = users[0].Id, IsHost = true },
            new() { UserId = users[2].Id }
        ]
    },
    new()
    {
        Title = "Faggruppe Møte",
        Date = DateTime.Now.AddMonths(3),
        Description = "Diskutere fagstoff og forberede eksamener",
        Category = "subjectGroup",
        City = "Oslo",
        LocationName = "BI Norwegian Business School, Oslo, Norge",
        Latitude = 59.9200,
        Longitude = 10.7300,
        University = "BI Norwegian Business School",
        Attendees =
        [
            new() { UserId = users[1].Id, IsHost = true }
        ]
    }
};

        await context.Activities.AddRangeAsync(activities);
        await context.SaveChangesAsync();
    }
}
