using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using soft20181_starter.Models;

namespace soft20181_starter.Pages.AllEvents
{
    public class IndexModel : PageModel
    {
        public List<Event> EventInfo { get; set; }

        private readonly EventAppDbContext dbContext;

        [BindProperty(SupportsGet = true)]
        public bool booked { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool added { get; set; }

        public List<String> Categories{ get; set; }

        public IndexModel(EventAppDbContext _db)
        {
            dbContext = _db;
        }
        public void OnGet()
        {

            EventInfo = dbContext.Events.ToList();
            Categories = dbContext.Events.Where(e => e.Category != null).Select(e => e.Category.Trim()).Distinct().ToList();

        }
    }
}
