using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using soft20181_starter.Models;

namespace soft20181_starter.Pages.ManageUser
{
    [Authorize(Roles = "Admin")]
    public class ViewUsersModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly EventAppDbContext dbContext;

        [BindProperty(SupportsGet = true)]
        public bool ChangePassword{ get; set; }

        [BindProperty(SupportsGet = true)]
        public bool Delete { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool ChangeRole { get; set; }

        public List<User> Users { get; set; }

        public ViewUsersModel(EventAppDbContext _dbContext, UserManager<User> userManager)
        {
            dbContext = _dbContext;
            _userManager = userManager;
        }
        public void OnGet()
        {
            Users = dbContext.Users.ToList();
        }

    }
}
