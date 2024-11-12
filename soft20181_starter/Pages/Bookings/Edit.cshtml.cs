using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using soft20181_starter.Models;

namespace soft20181_starter.Pages.Bookings
{
    [Authorize]
    public class EditModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int id { get; set; }

        [BindProperty(SupportsGet = true)]
        public string userid { get; set; }

        [BindProperty]
        public Booking TheBooking { get; set; }

        public Event Event { get; set; }

        public User TheUser { get; set; }

        private readonly UserManager<User> _userManager;

        private readonly EventAppDbContext dbContext;

        public EditModel(EventAppDbContext dbContext, UserManager<User> userManager)
        {
            this.dbContext = dbContext;
            _userManager = userManager;
        }

        public IActionResult OnPost()
        {

            dbContext.Update(TheBooking);
            dbContext.SaveChanges();
            if (userid != null)
            {
                return RedirectToPage("./ViewBookedUsersByEvent", new {edit = true, id = this.id});
            }
            else
            {
                return RedirectToPage("./View", new { edit = true });
            }
        }
        public void OnGet()
        {
            if (userid != null)
            {
                TheUser = dbContext.Users.Find(userid);
            }
            else
            {
                TheUser = _userManager.FindByNameAsync(User.Identity.Name).Result;
            }
            TheBooking = dbContext.Bookings.Find(TheUser.Id, id);
            Event = dbContext.Events.Find(TheBooking.EventId);
        }
    }
}
