using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using soft20181_starter.Models;

namespace soft20181_starter.Pages
{
    [Authorize(Roles = "Admin")]
    public class AdminPortalModel : PageModel
    {
        public List<Event> Events { get; set; }

        private readonly EventAppDbContext dbContext;

        public AdminPortalModel(EventAppDbContext _db)
        {
            dbContext = _db;
        }

        public void OnGet()
        {
            Events = dbContext.Events.ToList();
        }
    }
}
