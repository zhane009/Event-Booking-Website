using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using soft20181_starter.Models;
using Microsoft.AspNetCore.Identity;
using IronPdf;
using IronPdf.Razor.Pages;
using IronPdf.Rendering;
using IronPdf.Extensions.Mvc.Core;

namespace soft20181_starter.Pages.Bookings
{
    [Authorize]
    public class ViewModel : PageModel
    {
        public User TheUser { get; set; }

        [BindProperty(SupportsGet = true)]
        public int id { get; set; }

        [BindProperty(SupportsGet = true)]
        public string userid { get; set; }
        

        [BindProperty(SupportsGet = true)]
        public bool delete { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public bool edit { get; set; }
        public List<Booking> Bookings { get; set; }

        public List<Event> BookedEventsList { get; set; } = new List<Event>();

        private readonly EventAppDbContext dbContext;

        private readonly UserManager<User> _userManager;

        public ViewModel(EventAppDbContext _db, UserManager<User> userManager)
        {
            dbContext = _db;
            _userManager = userManager;
        }
        public void OnGet()
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name);
            TheUser = user.Result;

            var allBookings = dbContext.Bookings.Where(b => b.UserId == TheUser.Id);
            Bookings = allBookings.ToList();
            Bookings.ForEach(b =>
            {
                Event bookedEvent = dbContext.Events.Find(b.EventId);
                BookedEventsList.Add(bookedEvent);
            });
        }

        public IActionResult OnGetDelete()
        {
            if (userid != null)
            {
                var user = dbContext.Users.Find(userid);
                Booking booking = new Booking(user.Id, id);
                dbContext.Bookings.Remove(dbContext.Bookings.Find(booking.UserId, booking.EventId));
                dbContext.SaveChanges();
                return RedirectToPage("/Bookings/ViewBookedUsersByEvent", new { delete = true, id = this.id });
            }
            else
            {
                var user = _userManager.FindByNameAsync(User.Identity.Name);
                TheUser = user.Result;

                Booking booking = new Booking(TheUser.Id, id);

                dbContext.Bookings.Remove(dbContext.Bookings.Find(booking.UserId, booking.EventId));
                dbContext.SaveChanges();
                return RedirectToPage("/Bookings/View", new { delete = true });
            }


        }

        public IActionResult OnPost()
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name);
            TheUser = user.Result;
            var allBookings = dbContext.Bookings.Where(b => b.UserId == TheUser.Id);
            Bookings = allBookings.ToList();
            Bookings.ForEach(b =>
            {
                Event bookedEvent = dbContext.Events.Find(b.EventId);
                BookedEventsList.Add(bookedEvent);
            });
            

            var renderer = new ChromePdfRenderer();
            string htmlContent = @"
    <h2>All Bookings for User - {0} -- {1} {2} -- {3}</h2>

    <table class='table'>
        <thead>
            <tr>
                <th scope='col'>#</th>
                <th scope='col'>Event Title</th>
                <th scope='col'>Location</th>
                <th scope='col'>Date</th>
                <th scope='col'>Time</th>
                <th scope='col'>Quantity</th>
            </tr>
        </thead>
        <tbody><br><br>";
            htmlContent = string.Format(htmlContent,
                TheUser.Email,
                TheUser.firstName,
                TheUser.lastName,
                TheUser.dateOfBirth);

            for (int i = 0; i < Bookings.Count; i++)
            {
                htmlContent += @"
            <tr>
                <th scope='row'>{0}</th>
                <td>{1}</td>
                <td>{2}</td>
                <td>{3}</td>
                <td>{4}</td>
                <td>{5}</td>
            </tr>";
                htmlContent = string.Format(htmlContent,
                    i+1,
                    BookedEventsList[i].Title,
                    BookedEventsList[i].Location,
                    BookedEventsList[i].Date,
                    BookedEventsList[i].Time,
                    Bookings[i].quantity
                    
                    );

            }

            

            htmlContent += @"
        </tbody>
    </table>";


            // Substitute placeholders with actual values
            

            renderer.RenderingOptions.CustomCssUrl = "https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css";
            PdfDocument pdf = renderer.RenderHtmlAsPdf(htmlContent);

            return File(pdf.BinaryData, "application/pdf", TheUser.Email + "_attendence_list.pdf");
        }
    }
}
