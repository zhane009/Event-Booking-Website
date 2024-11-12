using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using soft20181_starter.Models;
using System.ComponentModel.DataAnnotations;

namespace soft20181_starter.Pages
{
    [Authorize]
    public class EventDetailsModel : PageModel
    {
        [BindProperty]
        public Event TheEvent { get; set; }

        [BindProperty(SupportsGet = true)]
        public int id { get; set; }

        [BindProperty]
        [Required]
        [Range(1, 10)]
        public int numberOfTickets { get; set; }

        public List<Event> AllEvents { get; set; }
        
        public List<Event> SimilarEvents { get; set; }

        public User TheUser { get; set; }

        public bool Booked { get; set; } = false;

        public bool Clicked { get; set; } = false;

        private readonly EventAppDbContext dbContext;

        private readonly UserManager<User> _userManager;

        public EventDetailsModel(EventAppDbContext _db, UserManager<User> userManager)
        {
            dbContext = _db;
            _userManager = userManager;
        }

        public void OnGet()
        {
            SimilarEvents = new List<Event>();
            AllEvents = dbContext.Events.ToList();
            TheEvent = dbContext.Events.Find(id);

            foreach (var item in AllEvents)
            {
                if (item.Category == TheEvent.Category && item.Title != TheEvent.Title)
                {
                    SimilarEvents.Add(item);
                }
            }

            var user = _userManager.FindByNameAsync(User.Identity.Name);

            TheUser = user.Result;

            Booking TheBooking = new Booking(TheUser.Id, TheEvent.Id);
            if (dbContext.Bookings.Find(TheBooking.UserId, TheBooking.EventId) != null)
            {
                Booked = true;
            }
            else
            {
                Booked = false;
            }
        }

        /*public void OnGetBook()
        {
            TheEvent = dbContext.Events.Find(id);
            Clicked = true;
            var user = _userManager.FindByNameAsync(User.Identity.Name);

            TheUser = user.Result;
            Booking TheBooking = new Booking(TheUser.Id, TheEvent.Id, numberOfTickets);

            if (dbContext.Bookings.Find(TheBooking.UserId, TheBooking.EventId) != null)
            {
                Clicked = false;
                Booked = true;
            }
            else
            {
                dbContext.Bookings.Add(TheBooking);
                dbContext.SaveChanges();
                Booked = true;
            }

        }*/

        public IActionResult OnPost()
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name);
            
            TheUser = user.Result;
            
            Booking TheBooking = new Booking(TheUser.Id, TheEvent.Id, numberOfTickets);

            numberOfTickets = TheBooking.quantity;

            if (dbContext.Bookings.Find(TheBooking.UserId, TheBooking.EventId) != null)
            {
                Clicked = false;
                Booked = true;
            }
            else
            {
                dbContext.Bookings.Add(TheBooking);
                dbContext.SaveChanges();
                Booked = true;
            }

            return RedirectToPage("/AllEvents/Index", new { booked = Booked});
        }
    }
}
