using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Versioning;
using soft20181_starter.Models;

namespace soft20181_starter.Pages.Manage
{
    [Authorize(Roles = "Admin")]
    public class EditRoleModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string id { get; set; }
        
        [BindProperty]
        public IdentityRole Role { get; set; }
        
        [BindProperty]
        public string NewRole { get; set; }
        
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly EventAppDbContext  dbContext;
        private readonly UserManager<User> _userManager;

        public EditRoleModel(RoleManager<IdentityRole> roleManager, EventAppDbContext _db, UserManager<User> userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            dbContext = _db;
        }
        public void OnGet()
        {
            Role = _roleManager.FindByIdAsync(id).Result;
        }

        public IActionResult OnGetDelete()
        {
            Role = _roleManager.FindByIdAsync(id).Result;
            var usersWithRole = dbContext.Users.Where(u => u.roleName == Role.Name).ToList();
            var roles = _roleManager.Roles;
            foreach (var user in usersWithRole)
            {
                _userManager.RemoveFromRoleAsync(user, Role.Name);
                user.roleName = (roles.Any(r => r.Name != "Admin")) ? "Admin" : roles.FirstOrDefault(r => r.Name != "Admin").Name;
                _userManager.AddToRoleAsync(user, user.roleName);
                dbContext.Users.Update(user);
            }
            dbContext.Roles.Remove(Role);
            dbContext.SaveChanges();
            return RedirectToPage("ViewRoles", new {Delete = true});
        }

        public IActionResult OnPost()
        {
            Role = _roleManager.FindByNameAsync(Role.Name).Result;
            if (ModelState.IsValid)
            {
                var usersWithRole = dbContext.Users.Where(u => u.roleName == Role.Name).ToList();
                Role.Name = NewRole;
                dbContext.Roles.Update(Role);
                foreach (var user in usersWithRole)
                {
                    user.roleName = Role.Name;
                    dbContext.Users.Update(user);
                }
                dbContext.SaveChanges();
                return RedirectToPage("ViewRoles", new {Edit = true});
            }
            return Page();
        }
    }
}
