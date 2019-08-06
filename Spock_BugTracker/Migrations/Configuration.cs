namespace Spock_BugTracker.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Spock_BugTracker.Helpers;
    using Spock_BugTracker.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Spock_BugTracker.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Spock_BugTracker.Models.ApplicationDbContext context)
        {
            #region roleManager
            var roleManager = new RoleManager<IdentityRole>(
                    new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            if (!context.Roles.Any(r => r.Name == "ProjectManager"))
            {
                roleManager.Create(new IdentityRole { Name = "ProjectManager" });
            }


            if (!context.Roles.Any(r => r.Name == "Developer"))
            {
                roleManager.Create(new IdentityRole { Name = "Developer" });
            }

            if (!context.Roles.Any(r => r.Name == "Submitter"))
            {
                roleManager.Create(new IdentityRole { Name = "Submitter" });
            }
            #endregion

            #region User Generation
            //SeedHelper.GenerateUsers();
            var userManager = new UserManager<ApplicationUser>(
               new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "JTwichell@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "jTwichell@Mailinator.com",
                    Email = "JTwichell@Mailinator.com",
                    FirstName = "Jason",
                    LastName = "Twichell",
                    DisplayName = "Twich Man"
                }, "Abc&123!");
            }

            var userId = userManager.FindByEmail("JTwichell@Mailinator.com").Id;
            userManager.AddToRole(userId, "Admin");
            #endregion







            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.Projects.AddOrUpdate(
                p => p.Name,
                    new Project { Name = "Spock IT Blog", Description = "This is the Spock Blog project that is now out in the wild.", Created = DateTime.Now },
                    new Project { Name = "Spock Portfolio", Description = "This is the Portfolio project that is now out in the wild.", Created = DateTime.Now },
                    new Project { Name = "Spock BugTracker", Description = "This is the Spock BugTracker project that is now out in the wild.", Created = DateTime.Now }
                );

            context.TicketPriorities.AddOrUpdate(
                t => t.Name,
                    new TicketPriority { Name = "Immediate", Description = "Highest priority level requiring immediate action"},
                    new TicketPriority { Name = "High", Description = "A high priority level requiring quick action" },
                    new TicketPriority { Name = "Medium", Description = "" },
                    new TicketPriority { Name = "Low", Description = "" },
                    new TicketPriority { Name = "None", Description = "" }
                );

            context.TicketStatuses.AddOrUpdate(
                t => t.Name,
                    new TicketStatus { Name = "New / UnAssigned", Description = "" },
                    new TicketStatus { Name = "UnAssigned", Description = "" },
                    new TicketStatus { Name = "New / Assigned", Description = "" },
                    new TicketStatus { Name = "Assigned", Description = "" },
                    new TicketStatus { Name = "In Progress", Description = "" },
                    new TicketStatus { Name = "Completed", Description = "" },
                    new TicketStatus { Name = "Archived", Description = "" }
                );

            context.TicketTypes.AddOrUpdate(
                t => t.Name,
                    new TicketType { Name = "Bug / Defect", Description = ""},
                    new TicketType { Name = "New Functionality Request", Description = "" }, 
                    new TicketType { Name = "New Document Request", Description = "" }, 
                    new TicketType { Name = "Training Request", Description = "" }, 
                    new TicketType { Name = "Complaint", Description = "" }
                );
        }
    }
}
