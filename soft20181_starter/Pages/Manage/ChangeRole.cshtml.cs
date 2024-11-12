using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using soft20181_starter.Models;

namespace soft20181_starter.Pages.Manage
{
    [Authorize(Roles = "Admin")]
    public class ChangeRoleModel : PageModel
    {
        [BindProperty]
        public string NewRole { get; set; }

        [BindProperty]
        public User TheUser { get; set; }

        [BindProperty(SupportsGet = true)]
        public string id { get; set; }

        public List<String> Roles { get; set; }

        private readonly EventAppDbContext dbContext;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public ChangeRoleModel(EventAppDbContext _dbContext, UserManager<User> _userManager, RoleManager<IdentityRole> _roleManager)
        {
            dbContext = _dbContext;
            userManager = _userManager;
            roleManager = _roleManager;
        }   
        public void OnGet()
        {
            TheUser = dbContext.Users.Find(id);
            Roles = roleManager.Roles.Select(r => r.Name).ToList();
        }

        public IActionResult OnPost()
        {
            TheUser = userManager.FindByEmailAsync(TheUser.Email).Result;
            if (TheUser != null)
            {
                var roles = userManager.GetRolesAsync(TheUser).Result;
                if (roles.Count > 0)
                {
                    foreach (var role in roles)
                    {
                        userManager.RemoveFromRoleAsync(TheUser, role);
                    }
                }
                TheUser.roleName = NewRole;
                dbContext.Users.Update(TheUser);
                dbContext.SaveChanges();
                userManager.AddToRoleAsync(TheUser, NewRole);

                return RedirectToPage("ViewUsers", new { ChangeRole = true });
            }
            else
            {
                return Page();
            }
        }
    }
}
