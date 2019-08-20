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
    using System.Web.Configuration;

    internal sealed class Configuration : DbMigrationsConfiguration<Spock_BugTracker.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            #region Roles creation
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

            #region User creation
            var userManager = new UserManager<ApplicationUser>(
               new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "JasonTwichell@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "JasonTwichell@Mailinator.com",
                    Email = "JasonTwichell@Mailinator.com",
                    FirstName = "Jason",
                    LastName = "Twichell",
                    DisplayName = "Twich Man"
                }, "Abc&123!");
            }

            if (!context.Users.Any(u => u.Email == "Submitter@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "Submitter@Mailinator.com",
                    Email = "Submitter@Mailinator.com",
                    FirstName = "Sub",
                    LastName = "Mitter",
                    DisplayName = "Ticket Creator"
                }, "Abc&123!");
            }

            if (!context.Users.Any(u => u.Email == "Developer@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "Developer@Mailinator.com",
                    Email = "Developer@Mailinator.com",
                    FirstName = "John",
                    LastName = "Wick",
                    DisplayName = "Ticket Worker"
                }, "Abc&123!");
            }

            if (!context.Users.Any(u => u.Email == "ProjectManager@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "ProjectManager@Mailinator.com",
                    Email = "ProjectManager@Mailinator.com",
                    FirstName = "Project",
                    LastName = "Manager",
                    DisplayName = "The PM"
                }, "Abc&123!");
            }


            //Introduce my Demo Users...
            if (!context.Users.Any(u => u.Email == "DemoAdmin@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoAdmin@Mailinator.com",
                    Email = "DemoAdmin@Mailinator.com",
                    FirstName = "Demo",
                    LastName = "Admin",
                    DisplayName = "The Admin"
                }, WebConfigurationManager.AppSettings["DemoUserPassword"]);
            }

            if (!context.Users.Any(u => u.Email == "DemoProjectManager@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoProjectManager@Mailinator.com",
                    Email = "DemoProjectManager@Mailinator.com",
                    FirstName = "Demo",
                    LastName = "Project Manager",
                    DisplayName = "The PM"
                }, WebConfigurationManager.AppSettings["DemoUserPassword"]);
            }

            if (!context.Users.Any(u => u.Email == "DemoDeveloper@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoDeveloper@Mailinator.com",
                    Email = "DemoDeveloper@Mailinator.com",
                    FirstName = "Demo",
                    LastName = "Developer",
                    DisplayName = "The Dev"
                }, WebConfigurationManager.AppSettings["DemoUserPassword"]);
            }

            if (!context.Users.Any(u => u.Email == "DemoSubmitter@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoSubmitter@Mailinator.com",
                    Email = "DemoSubmitter@Mailinator.com",
                    FirstName = "Demo",
                    LastName = "Submitter",
                    DisplayName = "The Sub"
                }, WebConfigurationManager.AppSettings["DemoUserPassword"]);
            }
            #endregion

            #region Role Assignment
            var adminId = userManager.FindByEmail("JasonTwichell@Mailinator.com").Id;            
            userManager.AddToRole(adminId, "Admin");

            adminId = userManager.FindByEmail("DemoAdmin@Mailinator.com").Id;
            userManager.AddToRole(adminId, "Admin");

            var pmId = userManager.FindByEmail("ProjectManager@Mailinator.com").Id;
            userManager.AddToRole(pmId, "ProjectManager");

            pmId = userManager.FindByEmail("DemoProjectManager@Mailinator.com").Id;
            userManager.AddToRole(pmId, "ProjectManager");

            var subId = userManager.FindByEmail("Submitter@Mailinator.com").Id;
            userManager.AddToRole(subId, "Submitter");

            subId = userManager.FindByEmail("DemoSubmitter@Mailinator.com").Id;
            userManager.AddToRole(subId, "Submitter");


            var devId = userManager.FindByEmail("Developer@Mailinator.com").Id;
            userManager.AddToRole(devId, "Developer");

            devId = userManager.FindByEmail("DemoDeveloper@Mailinator.com").Id;
            userManager.AddToRole(devId, "Developer");
            #endregion

            #region Project Creation
            context.Projects.AddOrUpdate(
               p => p.Name,
                   new Project { Name = "Spock IT Blog", Description = "This is the Spock Blog project that is now out in the wild.", Created = DateTime.Now },
                   new Project { Name = "Spock Portfolio", Description = "This is the Portfolio project that is now out in the wild.", Created = DateTime.Now },
                   new Project { Name = "Spock BugTracker", Description = "This is the Spock BugTracker project that is now out in the wild.", Created = DateTime.Now }
               );

            context.SaveChanges();
            #endregion
          
            #region Project Assignment
            var blogProjectId = context.Projects.FirstOrDefault(p => p.Name == "Spock IT Blog").Id;
            var bugTrackerProjectId = context.Projects.FirstOrDefault(p => p.Name == "Spock BugTracker").Id;

            var projectHelper = new ProjectsHelper();

            //Assign all three users to the Blog project
            projectHelper.AddUserToProject(pmId, blogProjectId);
            projectHelper.AddUserToProject(devId, blogProjectId);
            projectHelper.AddUserToProject(subId, blogProjectId);

            //Assign all three users to the Blog project
            projectHelper.AddUserToProject(pmId, bugTrackerProjectId);
            projectHelper.AddUserToProject(devId, bugTrackerProjectId);
            projectHelper.AddUserToProject(subId, bugTrackerProjectId);
            #endregion

            #region Priority, Status & Type creation (required FK's for a Ticket)
            context.TicketPriorities.AddOrUpdate(
                t => t.Name,
                    new TicketPriority { Name = "Immediate", Description = "Highest priority level requiring immediate action" },
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
                    new TicketType { Name = "Bug", Description = "An error has occurred that resulted in either the application crashing or the user seeing error information" },
                    new TicketType { Name = "Defect", Description = "An error has occurred that resulted in either a miscalculation or an in correct workflow" },
                    new TicketType { Name = "Feature Request", Description = "A client has called in asking for new functionality in an existing application" },
                    new TicketType { Name = "Documentation Request", Description = "A client has called in asking for new documentation to be created for the existing application" },
                    new TicketType { Name = "Training Request", Description = "A client has called in asking to schedule a training session" },
                    new TicketType { Name = "Complaint", Description = "A client has called in to make a general complaint about our application" },
                    new TicketType { Name = "Other", Description = "A call has been received that requires follow up but is outside the usual parameters for a request" }
                );

            context.SaveChanges();
            #endregion

            #region Ticket creation          
            context.Tickets.AddOrUpdate(
               p => p.Title,
                //1 unassigned Bug on the Blog project
                //1 assigned Defect on the Blog project
                new Ticket
                {
                    ProjectId = blogProjectId,
                    OwnerUserId = subId,
                    Title = "Seeded Ticket #1",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Medium").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "New / UnAssigned").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Bug").Id,
                },
                new Ticket
                {
                    ProjectId = blogProjectId,
                    OwnerUserId = subId,
                    AssignedToUserId = devId,
                    Title = "Seeded Ticket #2",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Medium").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "New / Assigned").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Defect").Id,
                },

                //1 unassigned Bug on the BugTracker
                //1 assigned Defect on the BugTracker
                //1 unassigned Bug on the Blog project
                //1 assigned Defect on the Blog project
                new Ticket
                {
                    ProjectId = bugTrackerProjectId,
                    OwnerUserId = subId,
                    Title = "Seeded Ticket #3",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Medium").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "New / UnAssigned").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Bug").Id,
                },
                new Ticket
                {
                    ProjectId = bugTrackerProjectId,
                    OwnerUserId = subId,
                    AssignedToUserId = devId,
                    Title = "Seeded Ticket #4",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Medium").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "New / Assigned").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Defect").Id,
                });









            #endregion
        }
    }
}
