using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using soft20181_starter.Models;
using System.IO;

namespace soft20181_starter.Pages.AllEvents
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        [BindProperty]
        public Event TheEvent { get; set; }

        [BindProperty(SupportsGet = true)]
        public int id { get; set; }

        private readonly EventAppDbContext dbContext;

        public EditModel(EventAppDbContext _db)
        {
            dbContext = _db;
        }
        public void OnGet()
        {
            TheEvent = dbContext.Events.Find(id);
        }

        public IActionResult OnGetDelete()
        {
            TheEvent = dbContext.Events.Find(id);
            dbContext.Events.Remove(TheEvent);
            dbContext.SaveChanges();
            return RedirectToPage("Index", new { deleted = true });
        }

        public IActionResult OnPost()
        {
            /*            if (ModelState.IsValid)
                        {
                        }*/
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
            /*if (ModelState.IsValid)
            {
                dbContext.Events.Update(TheEvent);
                dbContext.SaveChanges();
                return RedirectToPage("Index", new { edited = true });
            }
            return Page();*/
            dbContext.Events.Update(TheEvent);
            dbContext.SaveChanges();
            return RedirectToPage("Index", new { edited = true });
        }
    }
}
