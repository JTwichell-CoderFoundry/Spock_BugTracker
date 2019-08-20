using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Spock_BugTracker.ViewModels
{
    public class UserIndexViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Avatar")]
        public string AvatarUrl { get; set; }

        [Display(Name ="Full Name")]
        public string FullName { get; set; }

        public string Email { get; set; }

        public IEnumerable<SelectListItem> CurrentRole { get; set; }

        public IEnumerable<SelectListItem> CurrentProjects { get; set; }

    }
}