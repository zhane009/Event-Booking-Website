using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using soft20181_starter.Models;

namespace soft20181_starter.Pages.ContactUs
{
    [Authorize(Roles = "Admin")]
    public class ViewContactsModel : PageModel
    {
        public List<Contact> ContactsInfo { get; set; }

        private readonly EventAppDbContext dbContext;

        [BindProperty(SupportsGet = true)]
        public bool delete { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool edited { get; set; }

        public ViewContactsModel(EventAppDbContext _db)
        {
            dbContext = _db;
        }
        public void OnGet()
        {
            ContactsInfo = dbContext.Contacts.ToList();
        }
    }
}
