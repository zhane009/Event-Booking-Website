using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using soft20181_starter.Models;

namespace soft20181_starter.Pages.Manage
{
    [Authorize(Roles = "Admin")]
    public class ViewBookingsCountByEventModel : PageModel
    {
        public List<Event> Events { get; set; }

        public List<int> Counts { get; set; }

        private readonly EventAppDbContext dbContext;  

        public ViewBookingsCountByEventModel(EventAppDbContext _db)
        {
            dbContext = _db;
        }

        public void OnGet()
        {
            Events = dbContext.Events.ToList();
            Counts = new List<int>();
            foreach (var e in Events)
            {
                Counts.Add(dbContext.Bookings.Where(b => b.EventId == e.Id).Count());
            }
        }
    }
}
