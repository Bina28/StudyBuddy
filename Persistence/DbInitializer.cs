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
        new() {DisplayName="Bob", UserName="bob@gmail.com", Email="bob@gmail.com"},
        new() {DisplayName="Tom", UserName="tom@gmail.com", Email="tom@gmail.com"},
        new() {DisplayName="Jane", UserName="jane@gmail.com", Email="jane@gmail.com"}
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
                Title = "Past Activity 1",
                Date = DateTime.Now.AddMonths(-2),
                Description = "Activity 2 months ago",
                Category = "drinks",
                City = "London",
                LocationName =
                    "The Lamb and Flag, 33, Rose Street, Seven Dials, Covent Garden, London, Greater London, England, WC2E 9EB, United Kingdom",
                Latitude = 51.51171665,
                Longitude = -0.1256611057818921,
                University = "University College London",
                Attendees =
                [
                    new()
                    {
                        UserId = users[0].Id,
                        IsHost = true,
                    },
                    new()
                    {
                        UserId = users[1].Id,
                        IsHost = false,
                    }
                ]
            },
            new()
            {
                Title = "Past Activity 2",
                Date = DateTime.Now.AddMonths(-1),
                Description = "Activity 1 month ago",
                Category = "culture",
                City = "Paris",
                LocationName =
                    "Louvre Museum, Rue Saint-Honoré, Quartier du Palais Royal, 1st Arrondissement, Paris, Ile-de-France, Metropolitan France, 75001, France",
                Latitude = 48.8611473,
                Longitude = 2.33802768704666,
                University = "Sorbonne University",
                Attendees =
                [
                    new()
                    {
                        UserId = users[1].Id,
                        IsHost = true,
                    },
                    new()
                    {
                        UserId = users[2].Id
                    },
                    new()
                    {
                        UserId = users[0].Id,
                    }
                ]
            },
            new()
            {
                Title = "Future Activity 1",
                Date = DateTime.Now.AddMonths(1),
                Description = "Activity 1 month in future",
                Category = "culture",
                City = "London",
                LocationName = "Natural History Museum",
                Latitude = 51.496510900000004,
                Longitude = -0.17600190725447445,
                University = "Imperial College London",
                Attendees =
                [
                    new()
                    {
                        UserId = users[2].Id,
                        IsHost = true,
                    }
                ]
            },
            new()
            {
                Title = "Future Activity 2",
                Date = DateTime.Now.AddMonths(2),
                Description = "Activity 2 months in future",
                Category = "music",
                City = "London",
                LocationName = "The O2",
                Latitude = 51.502936649999995,
                Longitude = 0.0032029278126681844,
                University = "King's College London",
                Attendees =
                [
                    new()
                    {
                        UserId = users[0].Id,
                        IsHost = true,
                    },
                    new()
                    {
                        UserId = users[2].Id
                    }
                ]
            },
            new()
            {
                Title = "Future Activity 3",
                Date = DateTime.Now.AddMonths(3),
                Description = "Activity 3 months in future",
                Category = "drinks",
                City = "London",
                LocationName = "The Mayflower",
                Latitude = 51.501778,
                Longitude = -0.053577,
                University = "London School of Economics",
                Attendees =
                [
                    new()
                    {
                        UserId = users[1].Id,
                        IsHost = true,
                    }
                ]
            },
            new()
            {
                Title = "Future Activity 4",
                Date = DateTime.Now.AddMonths(4),
                Description = "Activity 4 months in future",
                Category = "drinks",
                City = "London",
                LocationName = "The Blackfriar",
                Latitude = 51.512146650000005,
                Longitude = -0.10364680647106028,
                University = "Queen Mary University of London",
                Attendees =
                [
                    new()
                    {
                        UserId = users[2].Id,
                        IsHost = true,
                    },
                    new()
                    {
                        UserId = users[0].Id
                    }
                ]
            },
            new()
            {
                Title = "Future Activity 5",
                Date = DateTime.Now.AddMonths(5),
                Description = "Activity 5 months in future",
                Category = "culture",
                City = "London",
                LocationName =
                    "Sherlock Holmes Museum, 221b, Baker Street, Marylebone, London, Greater London, England, NW1 6XE, United Kingdom",
                Latitude = 51.5237629,
                Longitude = -0.1584743,
                University = "University of Westminster",
                Attendees =
                [
                    new()
                    {
                        UserId = users[0].Id,
                        IsHost = true,
                    }
                ]
            },
            new()
            {
                Title = "Future Activity 6",
                Date = DateTime.Now.AddMonths(6),
                Description = "Activity 6 months in future",
                Category = "music",
                City = "London",
                LocationName =
                    "Roundhouse, Chalk Farm Road, Maitland Park, Chalk Farm, London Borough of Camden, London, Greater London, England, NW1 8EH, United Kingdom",
                Latitude = 51.5432505,
                Longitude = -0.15197608174931165,
                University = "City, University of London",
                Attendees =
                [
                    new()
                    {
                        UserId = users[1].Id,
                        IsHost = true,
                    },
                    new()
                    {
                        UserId = users[0].Id
                    }
                ]
            },
            new()
            {
                Title = "Future Activity 7",
                Date = DateTime.Now.AddMonths(7),
                Description = "Activity 7 months in future",
                Category = "travel",
                City = "London",
                LocationName = "River Thames, England, United Kingdom",
                Latitude = 51.5575525,
                Longitude = -0.781404,
                University = "Brunel University London",
                Attendees =
                [
                    new()
                    {
                        UserId = users[2].Id,
                        IsHost = true,
                    },
                    new()
                    {
                        UserId = users[1].Id
                    }
                ]
            },
            new()
            {
                Title = "Future Activity 8",
                Date = DateTime.Now.AddMonths(8),
                Description = "Activity 8 months in future",
                Category = "film",
                City = "London",
                LocationName = "Odeon Leicester Square",
                Latitude = 51.5575525,
                Longitude = -0.781404,
                University = "University of the Arts London",
                Attendees =
                [
                    new()
                    {
                        UserId = users[0].Id,
                        IsHost = true,
                    }
                ]
            }
        };

        await context.Activities.AddRangeAsync(activities);
        await context.SaveChangesAsync();
    }
}
