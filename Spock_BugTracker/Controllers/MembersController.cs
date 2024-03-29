﻿using Microsoft.AspNet.Identity;
using Spock_BugTracker.Helpers;
using Spock_BugTracker.Models;
using Spock_BugTracker.ViewModels;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Spock_BugTracker.Controllers
{
    [RequireHttps]
    public class MembersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Members
        [Authorize]
        public ActionResult EditProfile()
        {
            var userId = User.Identity.GetUserId();

            var member = db.Users.Select(user => new UserProfileViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DisplayName = user.DisplayName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            }).FirstOrDefault(u => u.Id == userId);

            return View(member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(UserProfileViewModel member)
        {
            var user = db.Users.Find(member.Id);
            user.FirstName = member.FirstName;
            user.LastName = member.LastName;
            user.DisplayName = member.DisplayName;
            user.Email = member.Email;
            user.UserName = member.Email;
            user.PhoneNumber = member.PhoneNumber;
       
            if (ImageHelpers.IsWebFriendlyImage(member.Avatar))
            {
                var fileName = Path.GetFileName(member.Avatar.FileName);
                member.Avatar.SaveAs(Path.Combine(Server.MapPath("~/Avatars/"), fileName));
                user.AvatarUrl = "/Avatars/" + fileName;
            }

            db.SaveChanges();           

            return RedirectToAction("Dashboard", "Home");
        }



    }
}