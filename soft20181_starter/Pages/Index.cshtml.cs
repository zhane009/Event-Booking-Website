using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using soft20181_starter.Models;

namespace soft20181_starter.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly EventAppDbContext dbContext;

        public String[] Date { get; set; }

        public List<Event> EventInfo { get; set; }
        public IndexModel(ILogger<IndexModel> logger, EventAppDbContext _dbContext)
        {
            _logger = logger;
            dbContext = _dbContext;
        }

        public void OnGet()
        {
            EventInfo = dbContext.Events.ToList();
            
        }
    }
}
