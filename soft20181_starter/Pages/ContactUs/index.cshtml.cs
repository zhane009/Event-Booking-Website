using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using soft20181_starter.Models;
using System.Threading.Tasks;

namespace soft20181_starter.Pages.ContactUs
{

    public class ContactModel : PageModel
    {
        [BindProperty]
        public Contact ContactInfo { get; set; }

        public String Message { get; set; }

        public bool IsSuccess { get; set; } = false;

        private readonly EventAppDbContext dbContext;

        public ContactModel(EventAppDbContext _db)
        {
            dbContext = _db;
        }

        public void OnGet()
        {
            Message = "";
        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                dbContext.Contacts.Add(ContactInfo);
                dbContext.SaveChanges();
                Message = "Thank you " + ContactInfo.FirstName + ". We will be in touch shortly";
                IsSuccess = true;
            }


        }
    }
}
