using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using soft20181_starter.Models;

namespace soft20181_starter.Pages.ContactUs
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        [BindProperty]
        public Contact ContactInfo { get; set; }

        [BindProperty(SupportsGet = true)]
        public int id { get; set; }

        private readonly EventAppDbContext dbContext;
        public EditModel(EventAppDbContext _db)
        {
            dbContext = _db;
        }

        public void OnGet()
        {
            ContactInfo = dbContext.Contacts.Find(id);
        }

        public IActionResult OnGetDelete()
        {
            ContactInfo = dbContext.Contacts.Find(id);
            dbContext.Contacts.Remove(ContactInfo);
            dbContext.SaveChanges();
            return RedirectToPage("View", new { delete = true });
        }

        public IActionResult OnPost()
        {
            if (ContactInfo != null)
            {
                dbContext.Contacts.Update(ContactInfo);
                dbContext.SaveChanges(); 
                return RedirectToPage("View", new { edited = true });
            }
            else
            {
                return Page();
            }
        }
    }
}
