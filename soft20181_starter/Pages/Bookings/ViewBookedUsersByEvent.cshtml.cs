using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using soft20181_starter.Models;

namespace soft20181_starter.Pages.Bookings
{
    [Authorize(Roles = "Admin")]
    public class ViewBookedUsersByEvent : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int id { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool delete { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool edit { get; set; }

        public List<User> Users { get; set; }

        public Event TheEvent { get; set; }

        public List<Booking> Bookings { get; set; }

        private readonly EventAppDbContext dbContext;
        private readonly UserManager<User> _userManager;

        public ViewBookedUsersByEvent(EventAppDbContext _db, UserManager<User> userManager)
        {
            _userManager = userManager;
            dbContext = _db;
        }

        public void OnGet()
        {
            TheEvent = dbContext.Events.Find(id);
            Bookings = dbContext.Bookings.Where(b => b.EventId == id).ToList();
            Users = new List<User>();
            foreach (var booking in Bookings)
            {
                User TheUser = _userManager.FindByIdAsync(booking.UserId).Result;
                Users.Add(TheUser);
            }

        }


    }
}
