using System;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence;

public class DbInitializer
{
    public static async Task SeedData(AppDbContext context, UserManager<User> userManager)
    {
        if (!userManager.Users.Any())
        {
            var users = new List<User>
{
    new() {DisplayName="Bob", UserName="bob@gmail.com", Email="bob@gmail.com"},
    new() {DisplayName="Tom", UserName="tom@gmail.com", Email="tom@gmail.com"},
    new() {DisplayName="Jane", UserName="jane@gmail.com", Email="jane@gmail.com"}
};

            foreach (var user in users)
            {
                await userManager.CreateAsync(user, "Pa$$w0rd");
            }

        }

        if (context.Activities.Any()) return;

        var activities = new List<Activity>
{
 new() {
    Title = "Math Exam Study Group",
    Date = DateTime.UtcNow.AddDays(3),
    Description = "Let's work together on math problems before the exam.",
    Category = "mathGroup",
    IsCancelled = false,
    City = "Oslo",
    University = "University of Oslo",
    LocationName = "Blindern Library",
    Latitude = 59.9390,
    Longitude = 10.7210
},
new() {
    Title = "Programming Group Session",
    Date = DateTime.UtcNow.AddDays(5),
    Description = "Solving C# tasks together and helping each other out.",
    Category = "programmingGroup",
    IsCancelled = false,
    City = "Trondheim",
    University = "NTNU",
    LocationName = "Realfagsbygget",
    Latitude = 63.4185,
    Longitude = 10.4024
},
new() {
    Title = "Psychology Reading Circle",
    Date = DateTime.UtcNow.AddDays(7),
    Description = "Going through developmental psychology syllabus.",
    Category = "subjectGroup",
    IsCancelled = false,
    City = "Tromsø",
    University = "UiT - The Arctic University of Norway",
    LocationName = "University Library",
    Latitude = 69.6828,
    Longitude = 18.9570
},
new() {
    Title = "Philosophy Discussion Group",
    Date = DateTime.UtcNow.AddDays(2),
    Description = "Discussion on ethics and moral theories.",
    Category = "subjectGroup",
    IsCancelled = false,
    City = "Bergen",
    University = "University of Bergen",
    LocationName = "Humanities Library",
    Latitude = 60.3904,
    Longitude = 5.3224
},
new() {
    Title = "Oral Norwegian Exam Practice",
    Date = DateTime.UtcNow.AddDays(4),
    Description = "Training for oral exams and peer feedback.",
    Category = "language",
    IsCancelled = false,
    City = "Stavanger",
    University = "University of Stavanger",
    LocationName = "Learning Center",
    Latitude = 58.9363,
    Longitude = 5.7098
},
new() {
    Title = "Bachelor Thesis Brainstorming",
    Date = DateTime.UtcNow.AddDays(10),
    Description = "Get inspired and share ideas for your bachelor thesis.",
    Category = "subjectGroup",
    IsCancelled = false,
    City = "Kristiansand",
    University = "University of Agder",
    LocationName = "Grimstad Campus",
    Latitude = 58.3370,
    Longitude = 8.5934
},
new() {
    Title = "Co-writing Study Day",
    Date = DateTime.UtcNow.AddDays(6),
    Description = "Sit down and work on assignments together.",
    Category = "subjectGroup",
    IsCancelled = false,
    City = "Ås",
    University = "NMBU",
    LocationName = "Main Tower Building",
    Latitude = 59.6643,
    Longitude = 10.7847
},
new() {
    Title = "Language Exchange: Norwegian–English",
    Date = DateTime.UtcNow.AddDays(8),
    Description = "Learn Norwegian or English in a casual speaking group.",
    Category = "language",
    IsCancelled = false,
    City = "Drammen",
    University = "USN",
    LocationName = "Main Building - Meeting Room 2nd Floor",
    Latitude = 59.7425,
    Longitude = 10.2046
},
new() {
    Title = "Law Case Study Group",
    Date = DateTime.UtcNow.AddDays(9),
    Description = "Reviewing legal case studies before the exam.",
    Category = "subjectGroup",
    IsCancelled = false,
    City = "Bodø",
    University = "Nord University",
    LocationName = "Auditorium A2",
    Latitude = 67.2804,
    Longitude = 14.4049
},
new() {
    Title = "Weekend Study with Coffee",
    Date = DateTime.UtcNow.AddDays(1),
    Description = "Casual reading/study session at a café — all subjects welcome.",
    Category = "cafeMeeting",
    IsCancelled = false,
    City = "Lillehammer",
    University = "Inland Norway University of Applied Sciences",
    LocationName = "Cafe Stasjonen",
    Latitude = 61.1153,
    Longitude = 10.4650
}

};


        await context.Activities.AddRangeAsync(activities);
        await context.SaveChangesAsync();
    }
}
