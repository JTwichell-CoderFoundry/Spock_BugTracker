using Microsoft.AspNet.Identity;
using Spock_BugTracker.Enumerations;
using Spock_BugTracker.Models;
using System;
using System.Linq;
using System.Web;

namespace Spock_BugTracker.Helpers
{
    public class OldTicketAccessHelper : CommonHelper
    {   
        public bool TicketDetailIsViewableByUser(int ticketId)
        {        
            //How do I use User when I am outside of the Controller??
            var userId = HttpContext.Current.User.Identity.GetUserId();

            var roleName = RoleHelper.ListUserRoles(userId).FirstOrDefault();
            var systemRole = (SystemRole)Enum.Parse(typeof(SystemRole), roleName);

            switch (systemRole)
            {
                case SystemRole.Admin:
                    break;
                case SystemRole.ProjectManager:
                    break;
                case SystemRole.Developer:
                    break;
                case SystemRole.Submitter:
                    break;
            }

            return true;
        }

        public bool TicketIsEditableByUser(Ticket ticket)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var myRole = RoleHelper.ListUserRoles(userId).FirstOrDefault();

            switch(myRole)
            {
                case "Developer":
                    return ticket.AssignedToUserId == userId;
                case "Submitter":
                    return ticket.OwnerUserId == userId; 
                case "ProjectManager":
                    //var myProjects = projectHelper.ListUserProjects(userId);
                    //foreach(var project in myProjects)
                    //{
                    //    foreach(var projticket in project.Tickets)
                    //    {
                    //        if (projticket.Id == ticket.Id)
                    //            return true;
                    //    }
                    //}
                    //return false;
                    return db.Users.Find(userId).Projects.SelectMany(t => t.Tickets).Select(t => t.Id).Contains(ticket.Id);
                case "Admin":
                    return true;
                default:
                    return false;
            }
        }

        public static bool TicketTypeIsEditableByUser(string userId, int ticketId)
        {
            return true;
        }

        public static bool TicketStatusIsEditableByUser(string userId, int ticketId)
        {
            return true;
        }

        public static bool TicketPriorityIsEditableByUser(string userId, int ticketId)
        {
            return true;
        }

        public static bool TicketTitleIsEditableByUser(string userId, int ticketId)
        {
            return true;
        }

        public static bool TicketDescrptionIsEditableByUser(string userId, int ticketId)
        {
            return true;
        }

        public static bool TicketAssignedToUserIdIsEditableByUser(string userId, int ticketId)
        {
            return true;
        }
    }

   

}