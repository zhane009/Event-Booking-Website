using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using soft20181_starter.Models;

namespace soft20181_starter.Pages.AllEvents
{
    [Authorize(Roles = "Admin")]
    public class ViewModel : PageModel
    {
        public List<Event> Events { get; set; }

        private readonly EventAppDbContext dbContext;

        [BindProperty(SupportsGet = true)]
        public bool edited { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool added { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool deleted { get; set; }

        public ViewModel(EventAppDbContext _db)
        {
            dbContext = _db;
        }
        public void OnGet()
        {
            Events = dbContext.Events.ToList();
        }
    }
}
