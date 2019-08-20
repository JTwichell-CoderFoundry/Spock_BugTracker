using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Spock_BugTracker.ViewModels
{
    public class TicketViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int TicketPriorityId { get; set; }

        public IEnumerable<SelectListItem> CurrentPriority { get; set; }

        public DateTime Created { get; set; }
    }
}