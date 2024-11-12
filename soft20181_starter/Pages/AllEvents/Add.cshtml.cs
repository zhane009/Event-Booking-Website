using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using soft20181_starter.Models;

namespace soft20181_starter.Pages.AllEvents
{
    [Authorize(Roles = "Admin")]
    public class AddModel : PageModel
    {
        [BindProperty]
        public Event TheEvent { get; set; }

        public List<Event> Events { get; set; }

        public List<String> Categories { get; set; }

        private readonly EventAppDbContext dbContext;

        public AddModel(EventAppDbContext _db)
        {
            dbContext = _db;
        }

        public void OnGet()
        {
            Events = dbContext.Events.ToList();
            Categories = dbContext.Events.Where(e => e.Category != null).Select(e => e.Category).Distinct().ToList();
        }

        public IActionResult OnPost()
        {
            var ImageUpload = HttpContext.Request.Form.Files["ImageUpload"];
            if (ImageUpload != null)
            {
                TheEvent.Image = ImageUpload.FileName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", ImageUpload.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    ImageUpload.CopyTo(stream);
                }
            }
/*            if (ModelState.IsValid)
            {
                
            }
            else
            {
                return Page();
            }*/
            dbContext.Events.Update(TheEvent);
            dbContext.SaveChanges();
            return RedirectToPage("Index", new { added = true });
        }
    }
}
