using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using soft20181_starter.Models;
using System.ComponentModel.DataAnnotations;

namespace soft20181_starter.Pages.Manage
{
    [Authorize(Roles = "Admin")]
    public class AddRoleModel : PageModel
    {
        [BindProperty]
        [Required]
        [Display(Name = "Role Name")]
        [DataType(DataType.Text)]
        [StringLength(50, MinimumLength = 3)]
        public string NewRole { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool Exists { get; set; }

        private readonly EventAppDbContext dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AddRoleModel(EventAppDbContext _dbContext, RoleManager<IdentityRole> roleManager)
        {
            dbContext = _dbContext;
            _roleManager = roleManager;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole();
                if (_roleManager.FindByNameAsync(NewRole).Result != null)
                {
                    return RedirectToPage("AddRole", new {Exists = true});
                }
                role.Name = NewRole;
                dbContext.Roles.Add(role);
                dbContext.SaveChanges();
                return RedirectToPage("ViewRoles", new {Add = true});
            }
            return Page();
        }
    }
}
